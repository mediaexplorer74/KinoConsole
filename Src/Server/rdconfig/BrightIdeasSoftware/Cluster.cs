// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.Cluster
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class Cluster : ICluster, IComparable
  {
    private int count;
    private string displayLabel;
    private object clusterKey;

    public Cluster(object key)
    {
      this.Count = 1;
      this.ClusterKey = key;
    }

    public override string ToString() => this.DisplayLabel ?? "[empty]";

    public int Count
    {
      get => this.count;
      set => this.count = value;
    }

    public string DisplayLabel
    {
      get => this.displayLabel;
      set => this.displayLabel = value;
    }

    public object ClusterKey
    {
      get => this.clusterKey;
      set => this.clusterKey = value;
    }

    public int CompareTo(object other)
    {
      if (other == null || other == DBNull.Value || !(other is ICluster cluster))
        return 1;
      if (this.ClusterKey is string clusterKey1)
        return string.Compare(clusterKey1, cluster.ClusterKey as string, StringComparison.CurrentCultureIgnoreCase);
      return this.ClusterKey is IComparable clusterKey2 ? clusterKey2.CompareTo(cluster.ClusterKey) : -1;
    }
  }
}
