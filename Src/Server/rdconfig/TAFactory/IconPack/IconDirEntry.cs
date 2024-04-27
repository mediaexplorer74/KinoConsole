// Decompiled with JetBrains decompiler
// Type: TAFactory.IconPack.IconDirEntry
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Runtime.InteropServices;


namespace TAFactory.IconPack
{
  [StructLayout(LayoutKind.Sequential, Size = 16)]
  public struct IconDirEntry
  {
    public byte Width;
    public byte Height;
    public byte ColorCount;
    public byte Reserved;
    public short Planes;
    public short BitCount;
    public int BytesInRes;
    public int ImageOffset;

    public GroupIconDirEntry ToGroupIconDirEntry(int id)
    {
      return new GroupIconDirEntry()
      {
        Width = this.Width,
        Height = this.Height,
        ColorCount = this.ColorCount,
        Reserved = this.Reserved,
        Planes = this.Planes,
        BitCount = this.BitCount,
        BytesInRes = this.BytesInRes,
        ID = (short) id
      };
    }
  }
}
