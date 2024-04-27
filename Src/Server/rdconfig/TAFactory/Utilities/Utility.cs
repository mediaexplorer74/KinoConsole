// Decompiled with JetBrains decompiler
// Type: TAFactory.Utilities.Utility
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.IO;
using System.Runtime.InteropServices;


namespace TAFactory.Utilities
{
  public static class Utility
  {
    public static T ReadStructure<T>(Stream inputStream) where T : struct
    {
      int length = Marshal.SizeOf(typeof (T));
      byte[] numArray = new byte[length];
      inputStream.Read(numArray, 0, length);
      IntPtr num = Marshal.AllocHGlobal(length);
      Marshal.Copy(numArray, 0, num, length);
      object structure = Marshal.PtrToStructure(num, typeof (T));
      Marshal.FreeHGlobal(num);
      return (T) structure;
    }

    public static void WriteStructure<T>(Stream outputStream, T structure) where T : struct
    {
      int length = Marshal.SizeOf(typeof (T));
      byte[] numArray = new byte[length];
      IntPtr num = Marshal.AllocHGlobal(length);
      Marshal.StructureToPtr((object) structure, num, true);
      Marshal.Copy(num, numArray, 0, length);
      Marshal.FreeHGlobal(num);
      outputStream.Write(numArray, 0, length);
    }
  }
}
