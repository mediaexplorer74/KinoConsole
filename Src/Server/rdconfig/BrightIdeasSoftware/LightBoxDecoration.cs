// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.LightBoxDecoration
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class LightBoxDecoration : BorderDecoration
  {
    public LightBoxDecoration()
    {
      this.BoundsPadding = new Size(-1, 4);
      this.CornerRounding = 8f;
      this.FillBrush = (Brush) new SolidBrush(Color.FromArgb(72, Color.Black));
    }

    public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
    {
      if (!r.Contains(olv.PointToClient(Cursor.Position)))
        return;
      Rectangle rowBounds = this.RowBounds;
      if (rowBounds.IsEmpty)
      {
        if (olv.View != View.Tile)
          return;
        g.FillRectangle(this.FillBrush, r);
      }
      else
      {
        using (Region region = new Region(r))
        {
          rowBounds.Inflate(this.BoundsPadding);
          region.Exclude(this.GetRoundedRect((RectangleF) rowBounds, this.CornerRounding));
          Region clip = g.Clip;
          g.Clip = region;
          g.FillRectangle(this.FillBrush, r);
          g.Clip = clip;
        }
      }
    }
  }
}
