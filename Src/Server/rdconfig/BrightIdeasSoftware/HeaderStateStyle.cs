// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.HeaderStateStyle
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.ComponentModel;
using System.Drawing;


namespace BrightIdeasSoftware
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class HeaderStateStyle
  {
    private Font font;
    private Color foreColor;
    private Color backColor;
    private Color frameColor;
    private float frameWidth;

    [DefaultValue(null)]
    public Font Font
    {
      get => this.font;
      set => this.font = value;
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

    [DefaultValue(typeof (Color), "")]
    public Color FrameColor
    {
      get => this.frameColor;
      set => this.frameColor = value;
    }

    [DefaultValue(0.0f)]
    public float FrameWidth
    {
      get => this.frameWidth;
      set => this.frameWidth = value;
    }
  }
}
