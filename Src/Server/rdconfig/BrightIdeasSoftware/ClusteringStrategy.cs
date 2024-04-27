// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ClusteringStrategy
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;


namespace BrightIdeasSoftware
{
  public class ClusteringStrategy : IClusteringStrategy
  {
    public static string NULL_LABEL = "[null]";
    public static string EMPTY_LABEL = "[empty]";
    private static string defaultDisplayLabelFormatSingular = "{0} ({1} item)";
    private static string defaultDisplayLabelFormatPural = "{0} ({1} items)";
    private OLVColumn column;
    private string displayLabelFormatSingular;
    private string displayLabelFormatPural;

    public static string DefaultDisplayLabelFormatSingular
    {
      get => ClusteringStrategy.defaultDisplayLabelFormatSingular;
      set => ClusteringStrategy.defaultDisplayLabelFormatSingular = value;
    }

    public static string DefaultDisplayLabelFormatPlural
    {
      get => ClusteringStrategy.defaultDisplayLabelFormatPural;
      set => ClusteringStrategy.defaultDisplayLabelFormatPural = value;
    }

    public ClusteringStrategy()
    {
      this.DisplayLabelFormatSingular = ClusteringStrategy.DefaultDisplayLabelFormatSingular;
      this.DisplayLabelFormatPlural = ClusteringStrategy.DefaultDisplayLabelFormatPlural;
    }

    public OLVColumn Column
    {
      get => this.column;
      set => this.column = value;
    }

    public string DisplayLabelFormatSingular
    {
      get => this.displayLabelFormatSingular;
      set => this.displayLabelFormatSingular = value;
    }

    public string DisplayLabelFormatPlural
    {
      get => this.displayLabelFormatPural;
      set => this.displayLabelFormatPural = value;
    }

    public virtual object GetClusterKey(object model) => this.Column.GetValue(model);

    public virtual ICluster CreateCluster(object clusterKey) => (ICluster) new Cluster(clusterKey);

    public virtual string GetClusterDisplayLabel(ICluster cluster)
    {
      string s = this.Column.ValueToString(cluster.ClusterKey) ?? ClusteringStrategy.NULL_LABEL;
      if (string.IsNullOrEmpty(s))
        s = ClusteringStrategy.EMPTY_LABEL;
      return this.ApplyDisplayFormat(cluster, s);
    }

    public virtual IModelFilter CreateFilter(IList valuesChosenForFiltering)
    {
      return (IModelFilter) new OneOfFilter(new AspectGetterDelegate(this.GetClusterKey), (ICollection) valuesChosenForFiltering);
    }

    protected virtual string ApplyDisplayFormat(ICluster cluster, string s)
    {
      string format = cluster.Count == 1 ? this.DisplayLabelFormatSingular : this.DisplayLabelFormatPlural;
      return !string.IsNullOrEmpty(format) ? string.Format(format, (object) s, (object) cluster.Count) : s;
    }
  }
}
