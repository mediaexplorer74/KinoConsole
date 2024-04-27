// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.MultiImageRenderer
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;


namespace BrightIdeasSoftware
{
  public class MultiImageRenderer : BaseRenderer
  {
    private object imageSelector;
    private int maxNumberImages = 10;
    private int minimumValue;
    private int maximumValue = 100;

    public MultiImageRenderer()
    {
    }

    public MultiImageRenderer(object imageSelector, int maxImages, int minValue, int maxValue)
      : this()
    {
      this.ImageSelector = imageSelector;
      this.MaxNumberImages = maxImages;
      this.MinimumValue = minValue;
      this.MaximumValue = maxValue;
    }

    [Category("Behavior")]
    [Description("The index of the image that should be drawn")]
    [DefaultValue(-1)]
    public int ImageIndex
    {
      get => this.imageSelector is int ? (int) this.imageSelector : -1;
      set => this.imageSelector = (object) value;
    }

    [Description("The index of the image that should be drawn")]
    [DefaultValue(null)]
    [Category("Behavior")]
    public string ImageName
    {
      get => this.imageSelector as string;
      set => this.imageSelector = (object) value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object ImageSelector
    {
      get => this.imageSelector;
      set => this.imageSelector = value;
    }

    [Category("Behavior")]
    [DefaultValue(10)]
    [Description("The maximum number of images that this renderer should draw")]
    public int MaxNumberImages
    {
      get => this.maxNumberImages;
      set => this.maxNumberImages = value;
    }

    [DefaultValue(0)]
    [Description("Values less than or equal to this will have 0 images drawn")]
    [Category("Behavior")]
    public int MinimumValue
    {
      get => this.minimumValue;
      set => this.minimumValue = value;
    }

    [Description("Values greater than or equal to this will have MaxNumberImages images drawn")]
    [DefaultValue(100)]
    [Category("Behavior")]
    public int MaximumValue
    {
      get => this.maximumValue;
      set => this.maximumValue = value;
    }

    public override void Render(Graphics g, Rectangle r)
    {
      this.DrawBackground(g, r);
      r = this.ApplyCellPadding(r);
      Image image = this.GetImage(this.ImageSelector);
      if (image == null || !(this.Aspect is IConvertible aspect))
        return;
      double num1 = aspect.ToDouble((IFormatProvider) NumberFormatInfo.InvariantInfo);
      int num2 = num1 > (double) this.MinimumValue ? (num1 >= (double) this.MaximumValue ? this.MaxNumberImages : 1 + (int) ((double) this.MaxNumberImages * (num1 - (double) this.MinimumValue) / (double) this.MaximumValue)) : 0;
      int width = image.Width;
      int height = image.Height;
      if (r.Height < image.Height)
      {
        width = (int) ((double) image.Width * (double) r.Height / (double) image.Height);
        height = r.Height;
      }
      Rectangle inner = r with
      {
        Width = this.MaxNumberImages * (width + this.Spacing) - this.Spacing,
        Height = height
      };
      inner = this.AlignRectangle(r, inner);
      for (int index = 0; index < num2; ++index)
      {
        g.DrawImage(image, inner.X, inner.Y, width, height);
        inner.X += width + this.Spacing;
      }
    }

    protected override Rectangle HandleGetEditRectangle(
      Graphics g,
      Rectangle cellBounds,
      OLVListItem item,
      int subItemIndex,
      Size preferredSize)
    {
      return this.CalculatePaddedAlignedBounds(g, cellBounds, preferredSize);
    }
  }
}
