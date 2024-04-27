// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IItemStyle
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;


namespace BrightIdeasSoftware
{
  public interface IItemStyle
  {
    Font Font { get; set; }

    FontStyle FontStyle { get; set; }

    Color ForeColor { get; set; }

    Color BackColor { get; set; }
  }
}
