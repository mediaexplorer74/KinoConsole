// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.AbstractVirtualListDataSource
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class AbstractVirtualListDataSource : IVirtualListDataSource, IFilterableDataSource
  {
    protected VirtualObjectListView listView;

    public AbstractVirtualListDataSource(VirtualObjectListView listView)
    {
      this.listView = listView;
    }

    public virtual object GetNthObject(int n) => (object) null;

    public virtual int GetObjectCount() => -1;

    public virtual int GetObjectIndex(object model) => -1;

    public virtual void PrepareCache(int from, int to)
    {
    }

    public virtual int SearchText(string value, int first, int last, OLVColumn column) => -1;

    public virtual void Sort(OLVColumn column, SortOrder order)
    {
    }

    public virtual void AddObjects(ICollection modelObjects)
    {
    }

    public virtual void RemoveObjects(ICollection modelObjects)
    {
    }

    public virtual void SetObjects(IEnumerable collection)
    {
    }

    public static int DefaultSearchText(
      string value,
      int first,
      int last,
      OLVColumn column,
      IVirtualListDataSource source)
    {
      if (first <= last)
      {
        for (int n = first; n <= last; ++n)
        {
          if (column.GetStringValue(source.GetNthObject(n)).StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
            return n;
        }
      }
      else
      {
        for (int n = first; n >= last; --n)
        {
          if (column.GetStringValue(source.GetNthObject(n)).StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
            return n;
        }
      }
      return -1;
    }

    public virtual void ApplyFilters(IModelFilter modelFilter, IListFilter listFilter)
    {
    }
  }
}
