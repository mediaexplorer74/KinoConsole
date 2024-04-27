// Decompiled with JetBrains decompiler
// Type: TAFactory.IconPack.IconHelper
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using Microsoft.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using TAFactory.Utilities;


namespace TAFactory.IconPack
{
  public static class IconHelper
  {
    public static IconInfo GetIconInfo(Icon icon) => new IconInfo(icon);

    public static IconInfo GetIconInfo(string fileName) => new IconInfo(fileName);

    public static Icon ExtractIcon(string fileName, int iconIndex)
    {
      Icon icon = (Icon) null;
      try
      {
        icon = new Icon(Environment.ExpandEnvironmentVariables(fileName));
      }
      catch
      {
      }
      if (icon != null)
        return icon;
      using (IconExtractor iconExtractor = new IconExtractor(fileName))
        return iconExtractor.GetIconAt(iconIndex);
    }

    public static List<Icon> ExtractAllIcons(string fileName)
    {
      Icon icon = (Icon) null;
      List<Icon> allIcons = new List<Icon>();
      try
      {
        icon = new Icon(Environment.ExpandEnvironmentVariables(fileName));
      }
      catch
      {
      }
      if (icon != null)
      {
        allIcons.Add(icon);
        return allIcons;
      }
      using (IconExtractor iconExtractor = new IconExtractor(fileName))
      {
        for (int index = 0; index < iconExtractor.IconCount; ++index)
          allIcons.Add(iconExtractor.GetIconAt(index));
      }
      return allIcons;
    }

    public static List<Icon> SplitGroupIcon(Icon icon) => new IconInfo(icon).Images;

    public static Icon GetBestFitIcon(Icon icon)
    {
      IconInfo iconInfo = new IconInfo(icon);
      int bestFitIconIndex = iconInfo.GetBestFitIconIndex();
      return iconInfo.Images[bestFitIconIndex];
    }

    public static Icon GetBestFitIcon(Icon icon, Size desiredSize)
    {
      IconInfo iconInfo = new IconInfo(icon);
      int bestFitIconIndex = iconInfo.GetBestFitIconIndex(desiredSize);
      return iconInfo.Images[bestFitIconIndex];
    }

    public static Icon GetBestFitIcon(Icon icon, Size desiredSize, bool isMonochrome)
    {
      IconInfo iconInfo = new IconInfo(icon);
      int bestFitIconIndex = iconInfo.GetBestFitIconIndex(desiredSize, isMonochrome);
      return iconInfo.Images[bestFitIconIndex];
    }

    public static Icon ExtractBestFitIcon(string fileName, int iconIndex)
    {
      return IconHelper.GetBestFitIcon(IconHelper.ExtractIcon(fileName, iconIndex));
    }

    public static Icon ExtractBestFitIcon(string fileName, int iconIndex, Size desiredSize)
    {
      return IconHelper.GetBestFitIcon(IconHelper.ExtractIcon(fileName, iconIndex), desiredSize);
    }

    public static Icon ExtractBestFitIcon(
      string fileName,
      int iconIndex,
      Size desiredSize,
      bool isMonochrome)
    {
      return IconHelper.GetBestFitIcon(IconHelper.ExtractIcon(fileName, iconIndex), desiredSize, isMonochrome);
    }

    public static Icon GetAssociatedIcon(string fileName, IconFlags flags)
    {
      flags |= IconFlags.Icon;
      SHFILEINFO psfi = new SHFILEINFO();
      Win32.SHGetFileInfo(fileName, 0U, ref psfi, (uint) Marshal.SizeOf((object) psfi), (SHGetFileInfoFlags) flags);
      return psfi.hIcon == IntPtr.Zero ? (Icon) null : Icon.FromHandle(psfi.hIcon);
    }

    public static Icon GetAssociatedLargeIcon(string fileName)
    {
      return IconHelper.GetAssociatedIcon(fileName, IconFlags.LargeIcon);
    }

    public static Icon GetAssociatedSmallIcon(string fileName)
    {
      return IconHelper.GetAssociatedIcon(fileName, IconFlags.SmallIcon);
    }

    public static Icon Merge(params Icon[] icons)
    {
      List<IconInfo> iconInfoList = new List<IconInfo>(icons.Length);
      int num1 = 0;
      foreach (Icon icon in icons)
      {
        if (icon != null)
        {
          IconInfo iconInfo = new IconInfo(icon);
          iconInfoList.Add(iconInfo);
          num1 += iconInfo.Images.Count;
        }
      }
      if (iconInfoList.Count == 0)
        throw new ArgumentNullException(nameof (icons), "The icons list should contain at least one icon.");
      MemoryStream outputStream = new MemoryStream();
      int num2 = 0;
      int offset = IconInfo.SizeOfIconDir + num1 * IconInfo.SizeOfIconDirEntry;
      for (int index1 = 0; index1 < iconInfoList.Count; ++index1)
      {
        IconInfo iconInfo = iconInfoList[index1];
        if (index1 == 0)
        {
          IconDir iconDir = iconInfo.IconDir with
          {
            Count = (short) num1
          };
          outputStream.Seek(0L, SeekOrigin.Begin);
          Utility.WriteStructure<IconDir>((Stream) outputStream, iconDir);
        }
        for (int index2 = 0; index2 < iconInfo.Images.Count; ++index2)
        {
          IconDirEntry iconDirEntry = iconInfo.IconDirEntries[index2] with
          {
            ImageOffset = offset
          };
          outputStream.Seek((long) (IconInfo.SizeOfIconDir + num2 * IconInfo.SizeOfIconDirEntry), SeekOrigin.Begin);
          Utility.WriteStructure<IconDirEntry>((Stream) outputStream, iconDirEntry);
          outputStream.Seek((long) offset, SeekOrigin.Begin);
          outputStream.Write(iconInfo.RawData[index2], 0, iconDirEntry.BytesInRes);
          ++num2;
          offset += iconDirEntry.BytesInRes;
        }
      }
      outputStream.Seek(0L, SeekOrigin.Begin);
      Icon icon1 = new Icon((Stream) outputStream);
      outputStream.Close();
      return icon1;
    }
  }
}
