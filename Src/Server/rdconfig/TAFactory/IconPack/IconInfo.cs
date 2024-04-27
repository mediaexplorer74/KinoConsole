// Decompiled with JetBrains decompiler
// Type: TAFactory.IconPack.IconInfo
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
  [Serializable]
  public class IconInfo
  {
    public static int SizeOfIconDir = Marshal.SizeOf(typeof (IconDir));
    public static int SizeOfIconDirEntry = Marshal.SizeOf(typeof (IconDirEntry));
    public static int SizeOfGroupIconDir = Marshal.SizeOf(typeof (GroupIconDir));
    public static int SizeOfGroupIconDirEntry = Marshal.SizeOf(typeof (GroupIconDirEntry));
    private Icon _sourceIcon;
    private string _fileName;
    private List<Icon> _images;
    private int _bestFitIconIndex;
    private int _width;
    private int _height;
    private int _colorCount;
    private int _planes;
    private int _bitCount;
    private IconDir _iconDir;
    private GroupIconDir _groupIconDir;
    private List<IconDirEntry> _iconDirEntries;
    private List<GroupIconDirEntry> _groupIconDirEntries;
    private List<byte[]> _rawData;
    private byte[] _resourceRawData;

    public Icon SourceIcon
    {
      get => this._sourceIcon;
      private set => this._sourceIcon = value;
    }

    public string FileName
    {
      get => this._fileName;
      private set => this._fileName = value;
    }

    public List<Icon> Images
    {
      get => this._images;
      private set => this._images = value;
    }

    public bool IsMultiIcon => this.Images.Count > 1;

    public int BestFitIconIndex
    {
      get => this._bestFitIconIndex;
      private set => this._bestFitIconIndex = value;
    }

    public int Width
    {
      get => this._width;
      private set => this._width = value;
    }

    public int Height
    {
      get => this._height;
      private set => this._height = value;
    }

    public int ColorCount
    {
      get => this._colorCount;
      private set => this._colorCount = value;
    }

    public int Planes
    {
      get => this._planes;
      private set => this._planes = value;
    }

    public int BitCount
    {
      get => this._bitCount;
      private set => this._bitCount = value;
    }

    public int ColorDepth
    {
      get
      {
        if (this.BitCount != 0)
          return this.BitCount;
        return this.ColorCount == 0 ? 0 : (int) Math.Log((double) this.ColorCount, 2.0);
      }
    }

    public IconDir IconDir
    {
      get => this._iconDir;
      private set => this._iconDir = value;
    }

    public GroupIconDir GroupIconDir
    {
      get => this._groupIconDir;
      private set => this._groupIconDir = value;
    }

    public List<IconDirEntry> IconDirEntries
    {
      get => this._iconDirEntries;
      private set => this._iconDirEntries = value;
    }

    public List<GroupIconDirEntry> GroupIconDirEntries
    {
      get => this._groupIconDirEntries;
      private set => this._groupIconDirEntries = value;
    }

    public List<byte[]> RawData
    {
      get => this._rawData;
      private set => this._rawData = value;
    }

    public byte[] ResourceRawData
    {
      get => this._resourceRawData;
      set => this._resourceRawData = value;
    }

    public IconInfo(Icon icon)
    {
      this.FileName = (string) null;
      this.LoadIconInfo(icon);
    }

    public IconInfo(string fileName)
    {
      this.FileName = this.FileName;
      this.LoadIconInfo(new Icon(fileName));
    }

    public int GetBestFitIconIndex()
    {
      IntPtr num = Marshal.AllocHGlobal(this.ResourceRawData.Length);
      Marshal.Copy(this.ResourceRawData, 0, num, this.ResourceRawData.Length);
      try
      {
        return Win32.LookupIconIdFromDirectory(num, true);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    public int GetBestFitIconIndex(Size desiredSize)
    {
      return this.GetBestFitIconIndex(desiredSize, false);
    }

    public int GetBestFitIconIndex(Size desiredSize, bool isMonochrome)
    {
      LookupIconIdFromDirectoryExFlags Flags = LookupIconIdFromDirectoryExFlags.LR_DEFAULTCOLOR;
      if (isMonochrome)
        Flags = LookupIconIdFromDirectoryExFlags.LR_MONOCHROME;
      IntPtr num = Marshal.AllocHGlobal(this.ResourceRawData.Length);
      Marshal.Copy(this.ResourceRawData, 0, num, this.ResourceRawData.Length);
      try
      {
        return Win32.LookupIconIdFromDirectoryEx(num, true, desiredSize.Width, desiredSize.Height, Flags);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    private void LoadIconInfo(Icon icon)
    {
      this.SourceIcon = icon != null ? icon : throw new ArgumentNullException(nameof (icon));
      MemoryStream memoryStream = new MemoryStream();
      this.SourceIcon.Save((Stream) memoryStream);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      IconDir iconDir = Utility.ReadStructure<IconDir>((Stream) memoryStream);
      this.IconDir = iconDir;
      this.GroupIconDir = iconDir.ToGroupIconDir();
      this.Images = new List<Icon>((int) iconDir.Count);
      this.IconDirEntries = new List<IconDirEntry>((int) iconDir.Count);
      this.GroupIconDirEntries = new List<GroupIconDirEntry>((int) iconDir.Count);
      this.RawData = new List<byte[]>((int) iconDir.Count);
      IconDir structure1 = iconDir with { Count = 1 };
      for (int id = 0; id < (int) iconDir.Count; ++id)
      {
        memoryStream.Seek((long) (IconInfo.SizeOfIconDir + id * IconInfo.SizeOfIconDirEntry), SeekOrigin.Begin);
        IconDirEntry iconDirEntry = Utility.ReadStructure<IconDirEntry>((Stream) memoryStream);
        this.IconDirEntries.Add(iconDirEntry);
        this.GroupIconDirEntries.Add(iconDirEntry.ToGroupIconDirEntry(id));
        byte[] buffer = new byte[iconDirEntry.BytesInRes];
        memoryStream.Seek((long) iconDirEntry.ImageOffset, SeekOrigin.Begin);
        memoryStream.Read(buffer, 0, buffer.Length);
        this.RawData.Add(buffer);
        IconDirEntry structure2 = iconDirEntry with
        {
          ImageOffset = IconInfo.SizeOfIconDir + IconInfo.SizeOfIconDirEntry
        };
        MemoryStream outputStream = new MemoryStream();
        Utility.WriteStructure<IconDir>((Stream) outputStream, structure1);
        Utility.WriteStructure<IconDirEntry>((Stream) outputStream, structure2);
        outputStream.Write(buffer, 0, buffer.Length);
        outputStream.Seek(0L, SeekOrigin.Begin);
        Icon icon1 = new Icon((Stream) outputStream);
        outputStream.Close();
        this.Images.Add(icon1);
        if (iconDir.Count == (short) 1)
        {
          this.BestFitIconIndex = 0;
          this.Width = (int) iconDirEntry.Width;
          this.Height = (int) iconDirEntry.Height;
          this.ColorCount = (int) iconDirEntry.ColorCount;
          this.Planes = (int) iconDirEntry.Planes;
          this.BitCount = (int) iconDirEntry.BitCount;
        }
      }
      memoryStream.Close();
      this.ResourceRawData = this.GetIconResourceData();
      if (iconDir.Count <= (short) 1)
        return;
      this.BestFitIconIndex = this.GetBestFitIconIndex();
      this.Width = (int) this.IconDirEntries[this.BestFitIconIndex].Width;
      this.Height = (int) this.IconDirEntries[this.BestFitIconIndex].Height;
      this.ColorCount = (int) this.IconDirEntries[this.BestFitIconIndex].ColorCount;
      this.Planes = (int) this.IconDirEntries[this.BestFitIconIndex].Planes;
      this.BitCount = (int) this.IconDirEntries[this.BestFitIconIndex].BitCount;
    }

    private byte[] GetIconResourceData()
    {
      MemoryStream outputStream = new MemoryStream();
      Utility.WriteStructure<GroupIconDir>((Stream) outputStream, this.GroupIconDir);
      foreach (GroupIconDirEntry groupIconDirEntry in this.GroupIconDirEntries)
        Utility.WriteStructure<GroupIconDirEntry>((Stream) outputStream, groupIconDirEntry);
      return outputStream.ToArray();
    }
  }
}
