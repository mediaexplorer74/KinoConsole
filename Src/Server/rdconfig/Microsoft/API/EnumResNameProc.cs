// Decompiled with JetBrains decompiler
// Type: Microsoft.API.EnumResNameProc
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Runtime.InteropServices;


namespace Microsoft.API
{
  [UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Auto)]
  public delegate bool EnumResNameProc(
    IntPtr hModule,
    ResourceTypes lpszType,
    IntPtr lpszName,
    IntPtr lParam);
}
