// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.CompositeAllFilter
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections.Generic;


namespace BrightIdeasSoftware
{
  public class CompositeAllFilter : CompositeFilter
  {
    public CompositeAllFilter(List<IModelFilter> filters)
      : base((IEnumerable<IModelFilter>) filters)
    {
    }

    public override bool FilterObject(object modelObject)
    {
      foreach (IModelFilter filter in (IEnumerable<IModelFilter>) this.Filters)
      {
        if (!filter.Filter(modelObject))
          return false;
      }
      return true;
    }
  }
}
