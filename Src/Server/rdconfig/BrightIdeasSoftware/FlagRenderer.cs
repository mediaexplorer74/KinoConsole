// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FlagRenderer
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;


namespace BrightIdeasSoftware
{
  public class FlagRenderer : BaseRenderer
  {
    private List<int> keysInOrder = new List<int>();
    private Dictionary<int, object> imageMap = new Dictionary<int, object>();

    public void Add(object key, object imageSelector)
    {
      int int32 = ((IConvertible) key).ToInt32((IFormatProvider) NumberFormatInfo.InvariantInfo);
      this.imageMap[int32] = imageSelector;
      this.keysInOrder.Remove(int32);
      this.keysInOrder.Add(int32);
    }

    public override void Render(Graphics g, Rectangle r)
    {
      this.DrawBackground(g, r);
      if (!(this.Aspect is IConvertible aspect))
        return;
      r = this.ApplyCellPadding(r);
      int int32 = aspect.ToInt32((IFormatProvider) NumberFormatInfo.InvariantInfo);
      ArrayList imageSelectors = new ArrayList();
      foreach (int key in this.keysInOrder)
      {
        if ((int32 & key) == key)
        {
          Image image = this.GetImage(this.imageMap[key]);
          if (image != null)
            imageSelectors.Add((object) image);
        }
      }
      if (imageSelectors.Count <= 0)
        return;
      this.DrawImages(g, r, (ICollection) imageSelectors);
    }

    protected override void HandleHitTest(Graphics g, OlvListViewHitTestInfo hti, int x, int y)
    {
      if (!(this.Aspect is IConvertible aspect))
        return;
      int int32 = aspect.ToInt32((IFormatProvider) NumberFormatInfo.InvariantInfo);
      Point location = this.Bounds.Location;
      foreach (int key in this.keysInOrder)
      {
        if ((int32 & key) == key)
        {
          Image image = this.GetImage(this.imageMap[key]);
          if (image != null)
          {
            if (new Rectangle(location, image.Size).Contains(x, y))
            {
              hti.UserData = (object) key;
              break;
            }
            location.X += image.Width + this.Spacing;
          }
        }
      }
    }
  }
}
