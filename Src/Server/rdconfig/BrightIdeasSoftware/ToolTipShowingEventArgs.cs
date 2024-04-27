// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ToolTipShowingEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class ToolTipShowingEventArgs : CellEventArgs
  {
    private ToolTipControl toolTipControl;
    public string Text;
    public RightToLeft RightToLeft;
    public bool? IsBalloon;
    public Color? BackColor;
    public Color? ForeColor;
    public string Title;
    public ToolTipControl.StandardIcons? StandardIcon;
    public int? AutoPopDelay;
    public Font Font;

    public ToolTipControl ToolTipControl
    {
      get => this.toolTipControl;
      internal set => this.toolTipControl = value;
    }
  }
}
