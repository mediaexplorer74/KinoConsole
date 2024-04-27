// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.OneOfFilter
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;


namespace BrightIdeasSoftware
{
  public class OneOfFilter : IModelFilter
  {
    private AspectGetterDelegate valueGetter;
    private IList possibleValues;

    public OneOfFilter(AspectGetterDelegate valueGetter)
      : this(valueGetter, (ICollection) new ArrayList())
    {
    }

    public OneOfFilter(AspectGetterDelegate valueGetter, ICollection possibleValues)
    {
      this.ValueGetter = valueGetter;
      this.PossibleValues = (IList) new ArrayList(possibleValues);
    }

    public virtual AspectGetterDelegate ValueGetter
    {
      get => this.valueGetter;
      set => this.valueGetter = value;
    }

    public virtual IList PossibleValues
    {
      get => this.possibleValues;
      set => this.possibleValues = value;
    }

    public virtual bool Filter(object modelObject)
    {
      if (this.ValueGetter == null || this.PossibleValues == null || this.PossibleValues.Count == 0)
        return false;
      object result1 = this.ValueGetter(modelObject);
      IEnumerable enumerable = result1 as IEnumerable;
      if (result1 is string || enumerable == null)
        return this.DoesValueMatch(result1);
      foreach (object result2 in enumerable)
      {
        if (this.DoesValueMatch(result2))
          return true;
      }
      return false;
    }

    protected virtual bool DoesValueMatch(object result) => this.PossibleValues.Contains(result);
  }
}
