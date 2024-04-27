// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.DropTargetLocation
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  [Flags]
  public enum DropTargetLocation
  {
    None = 0,
    Background = 1,
    Item = 2,
    BetweenItems = 4,
    AboveItem = 8,
    BelowItem = 16, // 0x00000010
    SubItem = 32, // 0x00000020
    RightOfItem = 64, // 0x00000040
    LeftOfItem = 128, // 0x00000080
  }
}
