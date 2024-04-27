// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ImageOverlay
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Drawing;


namespace BrightIdeasSoftware
{
  [TypeConverter("BrightIdeasSoftware.Design.OverlayConverter")]
  public class ImageOverlay : ImageAdornment, ITransparentOverlay, IOverlay
  {
    private int insetX = 20;
    private int insetY = 20;

    public ImageOverlay() => this.Alignment = ContentAlignment.BottomRight;

    [DefaultValue(20)]
    [NotifyParentProperty(true)]
    [Category("ObjectListView")]
    [Description("The horizontal inset by which the position of the overlay will be adjusted")]
    public int InsetX
    {
      get => this.insetX;
      set => this.insetX = Math.Max(0, value);
    }

    [Description("Gets or sets the vertical inset by which the position of the overlay will be adjusted")]
    [Category("ObjectListView")]
    [NotifyParentProperty(true)]
    [DefaultValue(20)]
    public int InsetY
    {
      get => this.insetY;
      set => this.insetY = Math.Max(0, value);
    }

    public virtual void Draw(ObjectListView olv, Graphics g, Rectangle r)
    {
      Rectangle r1 = r;
      r1.Inflate(-this.InsetX, -this.InsetY);
      this.DrawImage(g, r1, this.Image, (int) byte.MaxValue);
    }
  }
}
