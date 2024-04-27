// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IClusteringStrategy
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;


namespace BrightIdeasSoftware
{
  public interface IClusteringStrategy
  {
    OLVColumn Column { get; set; }

    object GetClusterKey(object model);

    ICluster CreateCluster(object clusterKey);

    string GetClusterDisplayLabel(ICluster cluster);

    IModelFilter CreateFilter(IList valuesChosenForFiltering);
  }
}
