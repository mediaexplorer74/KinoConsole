// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ListFilter
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;


namespace BrightIdeasSoftware
{
  public class ListFilter : AbstractListFilter
  {
    private ListFilter.ListFilterDelegate function;

    public ListFilter(ListFilter.ListFilterDelegate function) => this.Function = function;

    public ListFilter.ListFilterDelegate Function
    {
      get => this.function;
      set => this.function = value;
    }

    public override IEnumerable Filter(IEnumerable modelObjects)
    {
      return this.Function == null ? modelObjects : this.Function(modelObjects);
    }

    public delegate IEnumerable ListFilterDelegate(IEnumerable rowObjects);
  }
}
