// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.TextDecoration
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;


namespace BrightIdeasSoftware
{
  public class TextDecoration : TextAdornment, IDecoration, IOverlay
  {
    private OLVListItem listItem;
    private OLVListSubItem subItem;

    public TextDecoration() => this.Alignment = ContentAlignment.MiddleRight;

    public TextDecoration(string text)
      : this()
    {
      this.Text = text;
    }

    public TextDecoration(string text, int transparency)
      : this()
    {
      this.Text = text;
      this.Transparency = transparency;
    }

    public TextDecoration(string text, ContentAlignment alignment)
      : this()
    {
      this.Text = text;
      this.Alignment = alignment;
    }

    public TextDecoration(string text, int transparency, ContentAlignment alignment)
      : this()
    {
      this.Text = text;
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
      this.DrawText(g, this.CalculateItemBounds(this.ListItem, this.SubItem));
    }
  }
}
