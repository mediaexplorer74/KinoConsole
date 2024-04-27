// Decompiled with JetBrains decompiler
// Type: TAFactory.IconPack.IconExtractor
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using Microsoft.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using TAFactory.Utilities;


namespace TAFactory.IconPack
{
  public class IconExtractor : IDisposable
  {
    private string _fileName;
    private IntPtr _moduleHandle;
    private List<ResourceName> _iconNamesList;
    private Dictionary<int, Icon> _iconCache;

    public string FileName
    {
      get => this._fileName;
      private set => this._fileName = value;
    }

    public IntPtr ModuleHandle
    {
      get => this._moduleHandle;
      private set => this._moduleHandle = value;
    }

    public List<ResourceName> IconNamesList
    {
      get => this._iconNamesList;
      private set => this._iconNamesList = value;
    }

    public int IconCount => this.IconNamesList.Count;

    private Dictionary<int, Icon> IconCache
    {
      get => this._iconCache;
      set => this._iconCache = value;
    }

    public IconExtractor(string fileName) => this.LoadLibrary(fileName);

    ~IconExtractor() => this.Dispose();

    public Icon GetIconAt(int index)
    {
      if (index < 0 || index >= this.IconCount)
      {
        if (this.IconCount > 0)
          throw new ArgumentOutOfRangeException(nameof (index), (object) index, "Index should be in the range (0-" + this.IconCount.ToString() + ").");
        throw new ArgumentOutOfRangeException(nameof (index), (object) index, "No icons in the list.");
      }
      if (!this.IconCache.ContainsKey(index))
        this.IconCache[index] = this.GetIconFromLib(index);
      return this.IconCache[index];
    }

    private void LoadLibrary(string fileName)
    {
      this.FileName = !string.IsNullOrEmpty(fileName) ? Environment.ExpandEnvironmentVariables(fileName) : throw new ArgumentNullException(nameof (fileName));
      this.ModuleHandle = Win32.LoadLibraryEx(Environment.ExpandEnvironmentVariables(this.FileName), IntPtr.Zero, LoadLibraryExFlags.LOAD_LIBRARY_AS_DATAFILE);
      if (this.ModuleHandle == IntPtr.Zero)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        switch ((GetLastErrorResult) lastWin32Error)
        {
          case GetLastErrorResult.ERROR_FILE_NOT_FOUND:
            throw new FileNotFoundException("File not found.", this.FileName);
          case GetLastErrorResult.ERROR_BAD_EXE_FORMAT:
            throw new ArgumentException("The file '" + this.FileName + "' is not a valid win32 executable or dll.");
          default:
            throw new Win32Exception(lastWin32Error);
        }
      }
      else
      {
        this.IconNamesList = new List<ResourceName>();
        this.IconCache = new Dictionary<int, Icon>();
        Win32.EnumResourceNames(this.ModuleHandle, ResourceTypes.RT_GROUP_ICON, new EnumResNameProc(this.EnumResourcesCallBack), IntPtr.Zero);
      }
    }

    private bool EnumResourcesCallBack(
      IntPtr hModule,
      ResourceTypes lpszType,
      IntPtr lpszName,
      IntPtr lParam)
    {
      if (lpszType == ResourceTypes.RT_GROUP_ICON)
        this.IconNamesList.Add(new ResourceName(lpszName));
      return true;
    }

    private Icon GetIconFromLib(int index)
    {
      using (MemoryStream inputStream = new MemoryStream(IconExtractor.GetResourceData(this.ModuleHandle, this.IconNamesList[index], ResourceTypes.RT_GROUP_ICON)))
      {
        using (MemoryStream outputStream = new MemoryStream())
        {
          GroupIconDir groupIconDir = Utility.ReadStructure<GroupIconDir>((Stream) inputStream);
          int count = (int) groupIconDir.Count;
          int num = IconInfo.SizeOfIconDir + count * IconInfo.SizeOfIconDirEntry;
          Utility.WriteStructure<IconDir>((Stream) outputStream, groupIconDir.ToIconDir());
          for (int index1 = 0; index1 < count; ++index1)
          {
            GroupIconDirEntry groupIconDirEntry = Utility.ReadStructure<GroupIconDirEntry>((Stream) inputStream);
            outputStream.Seek((long) (IconInfo.SizeOfIconDir + index1 * IconInfo.SizeOfIconDirEntry), SeekOrigin.Begin);
            Utility.WriteStructure<IconDirEntry>((Stream) outputStream, groupIconDirEntry.ToIconDirEntry(num));
            byte[] resourceData = IconExtractor.GetResourceData(this.ModuleHandle, (int) groupIconDirEntry.ID, ResourceTypes.RT_ICON);
            outputStream.Seek((long) num, SeekOrigin.Begin);
            outputStream.Write(resourceData, 0, resourceData.Length);
            num += resourceData.Length;
          }
          outputStream.Seek(0L, SeekOrigin.Begin);
          return new Icon((Stream) outputStream);
        }
      }
    }

    private static byte[] GetResourceData(
      IntPtr hModule,
      ResourceName resourceName,
      ResourceTypes resourceType)
    {
      IntPtr hResInfo = IntPtr.Zero;
      try
      {
        hResInfo = Win32.FindResource(hModule, resourceName.Value, resourceType);
      }
      finally
      {
        resourceName.Free();
      }
      IntPtr hResData = !(hResInfo == IntPtr.Zero) ? Win32.LoadResource(hModule, hResInfo) : throw new Win32Exception();
      IntPtr source = !(hResData == IntPtr.Zero) ? Win32.LockResource(hResData) : throw new Win32Exception();
      if (source == IntPtr.Zero)
        throw new Win32Exception();
      int length = Win32.SizeofResource(hModule, hResInfo);
      byte[] destination = length != 0 ? new byte[length] : throw new Win32Exception();
      Marshal.Copy(source, destination, 0, destination.Length);
      return destination;
    }

    private static byte[] GetResourceData(
      IntPtr hModule,
      int resourceId,
      ResourceTypes resourceType)
    {
      IntPtr resource = Win32.FindResource(hModule, (IntPtr) resourceId, resourceType);
      IntPtr hResData = !(resource == IntPtr.Zero) ? Win32.LoadResource(hModule, resource) : throw new Win32Exception();
      IntPtr source = !(hResData == IntPtr.Zero) ? Win32.LockResource(hResData) : throw new Win32Exception();
      if (source == IntPtr.Zero)
        throw new Win32Exception();
      int length = Win32.SizeofResource(hModule, resource);
      byte[] destination = length != 0 ? new byte[length] : throw new Win32Exception();
      Marshal.Copy(source, destination, 0, destination.Length);
      return destination;
    }

    public void Dispose()
    {
      if (this.ModuleHandle != IntPtr.Zero)
      {
        try
        {
          Win32.FreeLibrary(this.ModuleHandle);
        }
        catch
        {
        }
        this.ModuleHandle = IntPtr.Zero;
      }
      if (this.IconNamesList == null)
        return;
      this.IconNamesList.Clear();
    }
  }
}
