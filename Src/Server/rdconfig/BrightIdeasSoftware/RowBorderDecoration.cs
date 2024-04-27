// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.RowBorderDecoration
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;


namespace BrightIdeasSoftware
{
  public class RowBorderDecoration : BorderDecoration
  {
    private int leftColumn = -1;
    private int rightColumn = -1;

    public int LeftColumn
    {
      get => this.leftColumn;
      set => this.leftColumn = value;
    }

    public int RightColumn
    {
      get => this.rightColumn;
      set => this.rightColumn = value;
    }

    protected override Rectangle CalculateBounds()
    {
      Rectangle rowBounds = this.RowBounds;
      if (this.ListItem == null)
        return rowBounds;
      if (this.LeftColumn >= 0)
      {
        Rectangle subItemBounds = this.ListItem.GetSubItemBounds(this.LeftColumn);
        if (!subItemBounds.IsEmpty)
        {
          rowBounds.Width = rowBounds.Right - subItemBounds.Left;
          rowBounds.X = subItemBounds.Left;
        }
      }
      if (this.RightColumn >= 0)
      {
        Rectangle subItemBounds = this.ListItem.GetSubItemBounds(this.RightColumn);
        if (!subItemBounds.IsEmpty)
          rowBounds.Width = subItemBounds.Right - rowBounds.Left;
      }
      return rowBounds;
    }
  }
}
