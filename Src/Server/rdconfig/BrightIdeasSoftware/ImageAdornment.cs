// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ImageAdornment
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;


namespace BrightIdeasSoftware
{
  public class ImageAdornment : GraphicAdornment
  {
    private Image image;
    private bool shrinkToWidth;

    [Category("ObjectListView")]
    [Description("The image that will be drawn")]
    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    public Image Image
    {
      get => this.image;
      set => this.image = value;
    }

    [DefaultValue(false)]
    [Description("Will the image be shrunk to fit within its width?")]
    [Category("ObjectListView")]
    public bool ShrinkToWidth
    {
      get => this.shrinkToWidth;
      set => this.shrinkToWidth = value;
    }

    public virtual void DrawImage(Graphics g, Rectangle r)
    {
      if (this.ShrinkToWidth)
        this.DrawScaledImage(g, r, this.Image, this.Transparency);
      else
        this.DrawImage(g, r, this.Image, this.Transparency);
    }

    public virtual void DrawImage(Graphics g, Rectangle r, Image image, int transparency)
    {
      if (image == null)
        return;
      this.DrawImage(g, r, image, image.Size, transparency);
    }

    public virtual void DrawImage(
      Graphics g,
      Rectangle r,
      Image image,
      Size sz,
      int transparency)
    {
      if (image == null)
        return;
      Rectangle alignedRectangle = this.CreateAlignedRectangle(r, sz);
      try
      {
        this.ApplyRotation(g, alignedRectangle);
        this.DrawTransparentBitmap(g, alignedRectangle, image, transparency);
      }
      finally
      {
        this.UnapplyRotation(g);
      }
    }

    public virtual void DrawScaledImage(Graphics g, Rectangle r, Image image, int transparency)
    {
      if (image == null)
        return;
      Size size = image.Size;
      if (image.Width > r.Width)
      {
        float num = (float) r.Width / (float) image.Width;
        size.Height = (int) ((double) image.Height * (double) num);
        size.Width = r.Width - 1;
      }
      this.DrawImage(g, r, image, size, transparency);
    }

    protected virtual void DrawTransparentBitmap(
      Graphics g,
      Rectangle r,
      Image image,
      int transparency)
    {
      ImageAttributes imageAttr = (ImageAttributes) null;
      if (transparency != (int) byte.MaxValue)
      {
        imageAttr = new ImageAttributes();
        float[][] newColorMatrix = new float[5][]
        {
          new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
          new float[5]
          {
            0.0f,
            0.0f,
            0.0f,
            (float) transparency / (float) byte.MaxValue,
            0.0f
          },
          new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
        };
        imageAttr.SetColorMatrix(new ColorMatrix(newColorMatrix));
      }
      g.DrawImage(image, r, 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel, imageAttr);
    }
  }
}
