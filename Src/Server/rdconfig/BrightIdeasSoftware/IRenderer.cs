// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IRenderer
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public interface IRenderer
  {
    bool RenderItem(
      DrawListViewItemEventArgs e,
      Graphics g,
      Rectangle itemBounds,
      object rowObject);

    bool RenderSubItem(
      DrawListViewSubItemEventArgs e,
      Graphics g,
      Rectangle cellBounds,
      object rowObject);

    void HitTest(OlvListViewHitTestInfo hti, int x, int y);

    Rectangle GetEditRectangle(
      Graphics g,
      Rectangle cellBounds,
      OLVListItem item,
      int subItemIndex,
      Size preferredSize);
  }
}
