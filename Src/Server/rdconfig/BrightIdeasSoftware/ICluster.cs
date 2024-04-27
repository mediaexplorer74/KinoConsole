// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ICluster
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public interface ICluster : IComparable
  {
    int Count { get; set; }

    string DisplayLabel { get; set; }

    object ClusterKey { get; set; }
  }
}
