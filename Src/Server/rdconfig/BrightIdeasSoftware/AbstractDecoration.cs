// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.AbstractDecoration
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class AbstractDecoration : IDecoration, IOverlay
  {
    private OLVListItem listItem;
    private OLVListSubItem subItem;

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

    public Rectangle RowBounds => this.ListItem == null ? Rectangle.Empty : this.ListItem.Bounds;

    public Rectangle CellBounds
    {
      get
      {
        return this.ListItem == null || this.SubItem == null ? Rectangle.Empty : this.ListItem.GetSubItemBounds(this.ListItem.SubItems.IndexOf((ListViewItem.ListViewSubItem) this.SubItem));
      }
    }

    public virtual void Draw(ObjectListView olv, Graphics g, Rectangle r)
    {
    }
  }
}
