// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.GroupMask
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  [Flags]
  public enum GroupMask
  {
    LVGF_NONE = 0,
    LVGF_HEADER = 1,
    LVGF_FOOTER = 2,
    LVGF_STATE = 4,
    LVGF_ALIGN = 8,
    LVGF_GROUPID = 16, // 0x00000010
    LVGF_SUBTITLE = 256, // 0x00000100
    LVGF_TASK = 512, // 0x00000200
    LVGF_DESCRIPTIONTOP = 1024, // 0x00000400
    LVGF_DESCRIPTIONBOTTOM = 2048, // 0x00000800
    LVGF_TITLEIMAGE = 4096, // 0x00001000
    LVGF_EXTENDEDIMAGE = 8192, // 0x00002000
    LVGF_ITEMS = 16384, // 0x00004000
    LVGF_SUBSET = 32768, // 0x00008000
    LVGF_SUBSETITEMS = 65536, // 0x00010000
  }
}
