// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ModelFilter
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe


namespace BrightIdeasSoftware
{
  public class ModelFilter : IModelFilter
  {
    private System.Predicate<object> predicate;

    public ModelFilter(System.Predicate<object> predicate) => this.Predicate = predicate;

    protected System.Predicate<object> Predicate
    {
      get => this.predicate;
      set => this.predicate = value;
    }

    public virtual bool Filter(object modelObject)
    {
      return this.Predicate == null || this.Predicate(modelObject);
    }
  }
}
