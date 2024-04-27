// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.CompositeFilter
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections.Generic;


namespace BrightIdeasSoftware
{
  public abstract class CompositeFilter : IModelFilter
  {
    private IList<IModelFilter> filters = (IList<IModelFilter>) new List<IModelFilter>();

    public CompositeFilter()
    {
    }

    public CompositeFilter(IEnumerable<IModelFilter> filters)
    {
      foreach (IModelFilter filter in filters)
      {
        if (filter != null)
          this.Filters.Add(filter);
      }
    }

    public IList<IModelFilter> Filters
    {
      get => this.filters;
      set => this.filters = value;
    }

    public virtual bool Filter(object modelObject)
    {
      return this.Filters == null || this.Filters.Count == 0 || this.FilterObject(modelObject);
    }

    public abstract bool FilterObject(object modelObject);
  }
}
