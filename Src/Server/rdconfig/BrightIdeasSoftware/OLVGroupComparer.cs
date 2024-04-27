// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.OLVGroupComparer
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class OLVGroupComparer : IComparer<OLVGroup>
  {
    private SortOrder sortOrder;

    public OLVGroupComparer(SortOrder order) => this.sortOrder = order;

    public int Compare(OLVGroup x, OLVGroup y)
    {
      int num = x.SortValue == null || y.SortValue == null ? string.Compare(x.Header, y.Header, StringComparison.CurrentCultureIgnoreCase) : x.SortValue.CompareTo((object) y.SortValue);
      if (this.sortOrder == SortOrder.Descending)
        num = -num;
      return num;
    }
  }
}
