// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.HyperlinkStyle
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class HyperlinkStyle : Component
  {
    private CellStyle normalStyle;
    private CellStyle overStyle;
    private CellStyle visitedStyle;
    private Cursor overCursor;

    public HyperlinkStyle()
    {
      this.Normal = new CellStyle();
      this.Normal.ForeColor = Color.Blue;
      this.Over = new CellStyle();
      this.Over.FontStyle = FontStyle.Underline;
      this.Visited = new CellStyle();
      this.Visited.ForeColor = Color.Purple;
      this.OverCursor = Cursors.Hand;
    }

    [Description("How should hyperlinks be drawn")]
    [Category("Appearance")]
    public CellStyle Normal
    {
      get => this.normalStyle;
      set => this.normalStyle = value;
    }

    [Description("How should hyperlinks be drawn when the mouse is over them?")]
    [Category("Appearance")]
    public CellStyle Over
    {
      get => this.overStyle;
      set => this.overStyle = value;
    }

    [Description("How should hyperlinks be drawn after they have been clicked")]
    [Category("Appearance")]
    public CellStyle Visited
    {
      get => this.visitedStyle;
      set => this.visitedStyle = value;
    }

    [Category("Appearance")]
    [Description("What cursor should be shown when the mouse is over a link?")]
    public Cursor OverCursor
    {
      get => this.overCursor;
      set => this.overCursor = value;
    }
  }
}
