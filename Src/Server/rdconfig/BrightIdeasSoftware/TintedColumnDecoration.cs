// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.TintedColumnDecoration
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class TintedColumnDecoration : AbstractDecoration
  {
    private OLVColumn columnToTint;
    private Color tint;
    private SolidBrush tintBrush;

    public TintedColumnDecoration() => this.Tint = Color.FromArgb(15, Color.Blue);

    public TintedColumnDecoration(OLVColumn column)
      : this()
    {
      this.ColumnToTint = column;
    }

    public OLVColumn ColumnToTint
    {
      get => this.columnToTint;
      set => this.columnToTint = value;
    }

    public Color Tint
    {
      get => this.tint;
      set
      {
        if (this.tint == value)
          return;
        if (this.tintBrush != null)
        {
          this.tintBrush.Dispose();
          this.tintBrush = (SolidBrush) null;
        }
        this.tint = value;
        this.tintBrush = new SolidBrush(this.tint);
      }
    }

    public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
    {
      if (olv.View != View.Details || olv.GetItemCount() == 0)
        return;
      OLVColumn olvColumn = this.ColumnToTint ?? olv.SelectedColumn;
      if (olvColumn == null)
        return;
      Point scrolledColumnSides = NativeMethods.GetScrolledColumnSides((ListView) olv, olvColumn.Index);
      if (scrolledColumnSides.X == -1)
        return;
      Rectangle rect = new Rectangle(scrolledColumnSides.X, r.Top, scrolledColumnSides.Y - scrolledColumnSides.X, r.Bottom);
      OLVListItem itemInDisplayOrder = olv.GetLastItemInDisplayOrder();
      if (itemInDisplayOrder != null)
      {
        Rectangle bounds = itemInDisplayOrder.Bounds;
        if (!bounds.IsEmpty && bounds.Bottom < rect.Bottom)
          rect.Height = bounds.Bottom - rect.Top;
      }
      g.FillRectangle((Brush) this.tintBrush, rect);
    }
  }
}
