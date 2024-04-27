// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.GroupState
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  [Flags]
  public enum GroupState
  {
    LVGS_NORMAL = 0,
    LVGS_COLLAPSED = 1,
    LVGS_HIDDEN = 2,
    LVGS_NOHEADER = 4,
    LVGS_COLLAPSIBLE = 8,
    LVGS_FOCUSED = 16, // 0x00000010
    LVGS_SELECTED = 32, // 0x00000020
    LVGS_SUBSETED = 64, // 0x00000040
    LVGS_SUBSETLINKFOCUSED = 128, // 0x00000080
    LVGS_ALL = 65535, // 0x0000FFFF
  }
}
