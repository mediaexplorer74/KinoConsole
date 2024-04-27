// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ModelObjectComparer
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class ModelObjectComparer : IComparer, IComparer<object>
  {
    private OLVColumn column;
    private SortOrder sortOrder;
    private ModelObjectComparer secondComparer;

    public ModelObjectComparer(OLVColumn col, SortOrder order)
    {
      this.column = col;
      this.sortOrder = order;
    }

    public ModelObjectComparer(OLVColumn col, SortOrder order, OLVColumn col2, SortOrder order2)
      : this(col, order)
    {
      if (col == col2 || col2 == null || order2 == SortOrder.None)
        return;
      this.secondComparer = new ModelObjectComparer(col2, order2);
    }

    public int Compare(object x, object y)
    {
      object x1 = this.column.GetValue(x);
      object y1 = this.column.GetValue(y);
      if (this.sortOrder == SortOrder.None)
        return 0;
      bool flag1 = x1 == null || x1 == DBNull.Value;
      bool flag2 = y1 == null || y1 == DBNull.Value;
      int num = flag1 || flag2 ? (!flag1 || !flag2 ? (flag1 ? -1 : 1) : 0) : this.CompareValues(x1, y1);
      if (this.sortOrder == SortOrder.Descending)
        num = -num;
      if (num == 0 && this.secondComparer != null)
        num = this.secondComparer.Compare(x, y);
      return num;
    }

    public int CompareValues(object x, object y)
    {
      switch (x)
      {
        case string strA:
          return string.Compare(strA, (string) y, StringComparison.CurrentCultureIgnoreCase);
        case IComparable comparable:
          return comparable.CompareTo(y);
        default:
          return 0;
      }
    }
  }
}
