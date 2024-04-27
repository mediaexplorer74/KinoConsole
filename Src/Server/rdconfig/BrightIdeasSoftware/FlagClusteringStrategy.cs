// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FlagClusteringStrategy
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;


namespace BrightIdeasSoftware
{
  public class FlagClusteringStrategy : ClusteringStrategy
  {
    private long[] values;
    private string[] labels;

    public FlagClusteringStrategy(Type enumType)
    {
      if ((object) enumType == null)
        throw new ArgumentNullException(nameof (enumType));
      if (!enumType.IsEnum)
        throw new ArgumentException("Type must be enum", nameof (enumType));
      if (enumType.GetCustomAttributes(typeof (FlagsAttribute), false) == null)
        throw new ArgumentException("Type must have [Flags] attribute", nameof (enumType));
      List<long> longList = new List<long>();
      foreach (object obj in Enum.GetValues(enumType))
        longList.Add(Convert.ToInt64(obj));
      List<string> stringList = new List<string>();
      foreach (string name in Enum.GetNames(enumType))
        stringList.Add(name);
      this.SetValues(longList.ToArray(), stringList.ToArray());
    }

    public FlagClusteringStrategy(long[] values, string[] labels) => this.SetValues(values, labels);

    public long[] Values
    {
      get => this.values;
      private set => this.values = value;
    }

    public string[] Labels
    {
      get => this.labels;
      private set => this.labels = value;
    }

    private void SetValues(long[] flags, string[] flagLabels)
    {
      if (flags == null || flags.Length == 0)
        throw new ArgumentNullException(nameof (flags));
      if (flagLabels == null || flagLabels.Length == 0)
        throw new ArgumentNullException(nameof (flagLabels));
      if (flags.Length != flagLabels.Length)
        throw new ArgumentException("values and labels must have the same number of entries", nameof (flags));
      this.Values = flags;
      this.Labels = flagLabels;
    }

    public override object GetClusterKey(object model)
    {
      List<long> clusterKey = new List<long>();
      try
      {
        long int64 = Convert.ToInt64(this.Column.GetValue(model));
        foreach (long num in this.Values)
        {
          if ((num & int64) == num)
            clusterKey.Add(num);
        }
        return (object) clusterKey;
      }
      catch (InvalidCastException ex)
      {
        return (object) clusterKey;
      }
      catch (FormatException ex)
      {
        return (object) clusterKey;
      }
    }

    public override string GetClusterDisplayLabel(ICluster cluster)
    {
      long int64 = Convert.ToInt64(cluster.ClusterKey);
      for (int index = 0; index < this.Values.Length; ++index)
      {
        if (int64 == this.Values[index])
          return this.ApplyDisplayFormat(cluster, this.Labels[index]);
      }
      return this.ApplyDisplayFormat(cluster, int64.ToString((IFormatProvider) CultureInfo.CurrentUICulture));
    }

    public override IModelFilter CreateFilter(IList valuesChosenForFiltering)
    {
      return (IModelFilter) new FlagBitSetFilter(new AspectGetterDelegate(((ClusteringStrategy) this).GetClusterKey), (ICollection) valuesChosenForFiltering);
    }
  }
}
