// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ImageDecoration
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;


namespace BrightIdeasSoftware
{
  public class ImageDecoration : ImageAdornment, IDecoration, IOverlay
  {
    private OLVListItem listItem;
    private OLVListSubItem subItem;

    public ImageDecoration() => this.Alignment = ContentAlignment.MiddleRight;

    public ImageDecoration(Image image)
      : this()
    {
      this.Image = image;
    }

    public ImageDecoration(Image image, int transparency)
      : this()
    {
      this.Image = image;
      this.Transparency = transparency;
    }

    public ImageDecoration(Image image, ContentAlignment alignment)
      : this()
    {
      this.Image = image;
      this.Alignment = alignment;
    }

    public ImageDecoration(Image image, int transparency, ContentAlignment alignment)
      : this()
    {
      this.Image = image;
      this.Transparency = transparency;
      this.Alignment = alignment;
    }

    public OLVListItem ListItem
    {
      get => this.listItem;
      set => this.listItem = value;
    }

    public OLVListSubItem SubItem
    {
      get => this.subItem;
      set => this.subItem = value;
    }

    public virtual void Draw(ObjectListView olv, Graphics g, Rectangle r)
    {
      this.DrawImage(g, this.CalculateItemBounds(this.ListItem, this.SubItem));
    }
  }
}
