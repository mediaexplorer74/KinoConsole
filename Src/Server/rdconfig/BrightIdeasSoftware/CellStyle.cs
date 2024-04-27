// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.CellStyle
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.ComponentModel;
using System.Drawing;


namespace BrightIdeasSoftware
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class CellStyle : IItemStyle
  {
    private Font font;
    private FontStyle fontStyle;
    private Color foreColor;
    private Color backColor;

    public Font Font
    {
      get => this.font;
      set => this.font = value;
    }

    [DefaultValue(FontStyle.Regular)]
    public FontStyle FontStyle
    {
      get => this.fontStyle;
      set => this.fontStyle = value;
    }

    [DefaultValue(typeof (Color), "")]
    public Color ForeColor
    {
      get => this.foreColor;
      set => this.foreColor = value;
    }

    [DefaultValue(typeof (Color), "")]
    public Color BackColor
    {
      get => this.backColor;
      set => this.backColor = value;
    }
  }
}
