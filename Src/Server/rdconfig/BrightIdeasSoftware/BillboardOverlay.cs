// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.BillboardOverlay
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Drawing;


namespace BrightIdeasSoftware
{
  public class BillboardOverlay : TextOverlay
  {
    private Point location;

    public BillboardOverlay()
    {
      this.Transparency = (int) byte.MaxValue;
      this.BackColor = Color.PeachPuff;
      this.TextColor = Color.Black;
      this.BorderColor = Color.Empty;
      this.Font = new Font("Tahoma", 10f);
    }

    public Point Location
    {
      get => this.location;
      set => this.location = value;
    }

    public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
    {
      if (string.IsNullOrEmpty(this.Text))
        return;
      Rectangle textBounds = this.CalculateTextBounds(g, r, this.Text) with
      {
        Location = this.Location
      };
      if (textBounds.Right > r.Width)
        textBounds.X = Math.Max(r.Left, r.Width - textBounds.Width);
      if (textBounds.Bottom > r.Height)
        textBounds.Y = Math.Max(r.Top, r.Height - textBounds.Height);
      this.DrawBorderedText(g, textBounds, this.Text, (int) byte.MaxValue);
    }
  }
}
