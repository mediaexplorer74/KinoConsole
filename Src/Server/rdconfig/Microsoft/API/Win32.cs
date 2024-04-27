// Decompiled with JetBrains decompiler
// Type: Microsoft.API.Win32
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Runtime.InteropServices;
using System.Text;


namespace Microsoft.API
{
  public static class Win32
  {
    public const int MAX_PATH = 260;

    public static bool IsIntResource(IntPtr lpszName) => (int) lpszName >>> 16 == 0;

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr LoadLibraryEx(
      string lpFileName,
      IntPtr hFile,
      LoadLibraryExFlags dwFlags);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumResourceNames(
      IntPtr hModule,
      ResourceTypes lpszType,
      EnumResNameProc lpEnumFunc,
      IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr FindResource(IntPtr hModule, IntPtr lpName, ResourceTypes lpType);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr LockResource(IntPtr hResData);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int SizeofResource(IntPtr hModule, IntPtr hResInfo);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int LookupIconIdFromDirectory(IntPtr presbits, bool fIcon);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int LookupIconIdFromDirectoryEx(
      IntPtr presbits,
      bool fIcon,
      int cxDesired,
      int cyDesired,
      LookupIconIdFromDirectoryExFlags Flags);

    [DllImport("user32.dll", EntryPoint = "LoadImageW", SetLastError = true)]
    public static extern IntPtr LoadImage(
      IntPtr hInstance,
      IntPtr lpszName,
      LoadImageTypes imageType,
      int cxDesired,
      int cyDesired,
      uint fuLoad);

    [DllImport("shell32.dll")]
    public static extern IntPtr SHGetFileInfo(
      string pszPath,
      uint dwFileAttributes,
      ref SHFILEINFO psfi,
      uint cbSizeFileInfo,
      SHGetFileInfoFlags uFlags);
  }
}
