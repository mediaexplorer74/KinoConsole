// Decompiled with JetBrains decompiler
// Type: TAFactory.IconPack.IconDir
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Runtime.InteropServices;


namespace TAFactory.IconPack
{
  [StructLayout(LayoutKind.Sequential, Size = 6)]
  public struct IconDir
  {
    public short Reserved;
    public short Type;
    public short Count;

    public GroupIconDir ToGroupIconDir()
    {
      return new GroupIconDir()
      {
        Reserved = this.Reserved,
        Type = this.Type,
        Count = this.Count
      };
    }
  }
}
