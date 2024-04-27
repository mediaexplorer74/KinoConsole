// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.TailFilter
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;


namespace BrightIdeasSoftware
{
  public class TailFilter : AbstractListFilter
  {
    private int count;

    public TailFilter()
    {
    }

    public TailFilter(int numberOfObjects) => this.Count = numberOfObjects;

    public int Count
    {
      get => this.count;
      set => this.count = value;
    }

    public override IEnumerable Filter(IEnumerable modelObjects)
    {
      if (this.Count <= 0)
        return modelObjects;
      ArrayList array = ObjectListView.EnumerableToArray(modelObjects, false);
      if (this.Count > array.Count)
        return (IEnumerable) array;
      object[] c = new object[this.Count];
      array.CopyTo(array.Count - this.Count, (Array) c, 0, this.Count);
      return (IEnumerable) new ArrayList((ICollection) c);
    }
  }
}
