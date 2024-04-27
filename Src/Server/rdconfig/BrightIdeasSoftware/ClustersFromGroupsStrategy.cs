// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ClustersFromGroupsStrategy
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe


namespace BrightIdeasSoftware
{
  public class ClustersFromGroupsStrategy : ClusteringStrategy
  {
    public override object GetClusterKey(object model) => this.Column.GetGroupKey(model);

    public override string GetClusterDisplayLabel(ICluster cluster)
    {
      string s = this.Column.ConvertGroupKeyToTitle(cluster.ClusterKey);
      if (string.IsNullOrEmpty(s))
        s = ClusteringStrategy.EMPTY_LABEL;
      return this.ApplyDisplayFormat(cluster, s);
    }
  }
}
