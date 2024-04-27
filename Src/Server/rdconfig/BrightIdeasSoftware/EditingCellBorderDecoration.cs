// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.EditingCellBorderDecoration
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;
using System.Drawing.Drawing2D;


namespace BrightIdeasSoftware
{
  public class EditingCellBorderDecoration : BorderDecoration
  {
    private bool useLightbox;

    public EditingCellBorderDecoration()
    {
      this.FillBrush = (Brush) null;
      this.BorderPen = new Pen(Color.DarkBlue, 2f);
      this.CornerRounding = 8f;
      this.BoundsPadding = new Size(10, 8);
    }

    public EditingCellBorderDecoration(bool useLightBox)
      : this()
    {
      this.UseLightbox = this.useLightbox;
    }

    public bool UseLightbox
    {
      get => this.useLightbox;
      set
      {
        if (this.useLightbox == value)
          return;
        this.useLightbox = value;
        if (!this.useLightbox || this.FillBrush != null)
          return;
        this.FillBrush = (Brush) new SolidBrush(Color.FromArgb(64, Color.Black));
      }
    }

    public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
    {
      if (!olv.IsCellEditing)
        return;
      Rectangle bounds = olv.CellEditor.Bounds;
      if (bounds.IsEmpty)
        return;
      bounds.Inflate(this.BoundsPadding);
      GraphicsPath roundedRect = this.GetRoundedRect((RectangleF) bounds, this.CornerRounding);
      if (this.FillBrush != null)
      {
        if (this.UseLightbox)
        {
          using (Region region = new Region(r))
          {
            region.Exclude(roundedRect);
            Region clip = g.Clip;
            g.Clip = region;
            g.FillRectangle(this.FillBrush, r);
            g.Clip = clip;
          }
        }
        else
          g.FillPath(this.FillBrush, roundedRect);
      }
      if (this.BorderPen == null)
        return;
      g.DrawPath(this.BorderPen, roundedRect);
    }
  }
}
