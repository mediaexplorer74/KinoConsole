// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FastListGroupingStrategy
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class FastListGroupingStrategy : AbstractVirtualGroups
  {
    private List<int> indexToGroupMap;

    public override IList<OLVGroup> GetGroups(GroupingParameters parmameters)
    {
      FastObjectListView folv = (FastObjectListView) parmameters.ListView;
      int capacity = 0;
      NullableDictionary<object, List<object>> nullableDictionary = new NullableDictionary<object, List<object>>();
      foreach (object filteredObject in folv.FilteredObjects)
      {
        object groupKey = parmameters.GroupByColumn.GetGroupKey(filteredObject);
        if (!nullableDictionary.ContainsKey(groupKey))
          nullableDictionary[groupKey] = new List<object>();
        nullableDictionary[groupKey].Add(filteredObject);
        ++capacity;
      }
      ModelObjectComparer modelObjectComparer = new ModelObjectComparer(parmameters.SortItemsByPrimaryColumn ? parmameters.ListView.GetColumn(0) : parmameters.PrimarySort, parmameters.PrimarySortOrder, parmameters.SecondarySort, parmameters.SecondarySortOrder);
      foreach (object key in (IEnumerable) nullableDictionary.Keys)
        nullableDictionary[key].Sort((IComparer<object>) modelObjectComparer);
      List<OLVGroup> groups = new List<OLVGroup>();
      foreach (object key in (IEnumerable) nullableDictionary.Keys)
      {
        string header = parmameters.GroupByColumn.ConvertGroupKeyToTitle(key);
        if (!string.IsNullOrEmpty(parmameters.TitleFormat))
        {
          int count = nullableDictionary[key].Count;
          string format = count == 1 ? parmameters.TitleSingularFormat : parmameters.TitleFormat;
          try
          {
            header = string.Format(format, (object) header, (object) count);
          }
          catch (FormatException ex)
          {
            header = "Invalid group format: " + format;
          }
        }
        OLVGroup group = new OLVGroup(header);
        group.Collapsible = folv.HasCollapsibleGroups;
        group.Key = key;
        group.SortValue = key as IComparable;
        group.Contents = (IList) nullableDictionary[key].ConvertAll<int>((Converter<object, int>) (x => folv.IndexOf(x)));
        group.VirtualItemCount = nullableDictionary[key].Count;
        if (parmameters.GroupByColumn.GroupFormatter != null)
          parmameters.GroupByColumn.GroupFormatter(group, parmameters);
        groups.Add(group);
      }
      if (parmameters.GroupByOrder != SortOrder.None)
        groups.Sort(parmameters.GroupComparer ?? (IComparer<OLVGroup>) new OLVGroupComparer(parmameters.GroupByOrder));
      this.indexToGroupMap = new List<int>(capacity);
      this.indexToGroupMap.AddRange((IEnumerable<int>) new int[capacity]);
      for (int index = 0; index < groups.Count; ++index)
      {
        foreach (int content in (List<int>) groups[index].Contents)
          this.indexToGroupMap[content] = index;
      }
      return (IList<OLVGroup>) groups;
    }

    public override int GetGroupMember(OLVGroup group, int indexWithinGroup)
    {
      return (int) group.Contents[indexWithinGroup];
    }

    public override int GetGroup(int itemIndex) => this.indexToGroupMap[itemIndex];

    public override int GetIndexWithinGroup(OLVGroup group, int itemIndex)
    {
      return group.Contents.IndexOf((object) itemIndex);
    }
  }
}
