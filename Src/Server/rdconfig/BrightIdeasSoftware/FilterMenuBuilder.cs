// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FilterMenuBuilder
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class FilterMenuBuilder
  {
    public static string APPLY_LABEL = "Apply";
    public static string CLEAR_ALL_FILTERS_LABEL = "Clear All Filters";
    public static string FILTERING_LABEL = "Filtering";
    public static string SELECT_ALL_LABEL = "Select All";
    public static Bitmap ClearFilteringImage = new Bitmap(128, 128);
    public static Bitmap FilteringImage = new Bitmap(128, 128);
    private bool treatNullAsDataValue = true;
    private int maxObjectsToConsider = 10000;
    private bool alreadyInHandleItemChecked;

    public bool TreatNullAsDataValue
    {
      get => this.treatNullAsDataValue;
      set => this.treatNullAsDataValue = value;
    }

    public int MaxObjectsToConsider
    {
      get => this.maxObjectsToConsider;
      set => this.maxObjectsToConsider = value;
    }

    public virtual ToolStripDropDown MakeFilterMenu(
      ToolStripDropDown strip,
      ObjectListView listView,
      OLVColumn column)
    {
      if (strip == null)
        throw new ArgumentNullException(nameof (strip));
      if (listView == null)
        throw new ArgumentNullException(nameof (listView));
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      if (!column.UseFiltering || column.ClusteringStrategy == null || listView.Objects == null)
        return strip;
      List<ICluster> clusters = this.Cluster(column.ClusteringStrategy, listView, column);
      if (clusters.Count > 0)
      {
        this.SortClusters(column.ClusteringStrategy, clusters);
        strip.Items.Add((ToolStripItem) this.CreateFilteringMenuItem(column, clusters));
      }
      return strip;
    }

    protected virtual List<ICluster> Cluster(
      IClusteringStrategy strategy,
      ObjectListView listView,
      OLVColumn column)
    {
      NullableDictionary<object, ICluster> map = new NullableDictionary<object, ICluster>();
      int num = 0;
      foreach (object model in listView.ObjectsForClustering)
      {
        this.ClusterOneModel(strategy, map, model);
        if (num++ > this.MaxObjectsToConsider)
          break;
      }
      foreach (ICluster cluster in (IEnumerable<ICluster>) map.Values)
        cluster.DisplayLabel = strategy.GetClusterDisplayLabel(cluster);
      return new List<ICluster>((IEnumerable<ICluster>) map.Values);
    }

    private void ClusterOneModel(
      IClusteringStrategy strategy,
      NullableDictionary<object, ICluster> map,
      object model)
    {
      object clusterKey = strategy.GetClusterKey(model);
      IEnumerable enumerable = clusterKey as IEnumerable;
      if (clusterKey is string || enumerable == null)
        enumerable = (IEnumerable) new object[1]
        {
          clusterKey
        };
      ArrayList arrayList = new ArrayList();
      foreach (object obj in enumerable)
      {
        if (obj == null || obj == DBNull.Value)
        {
          if (this.TreatNullAsDataValue)
            arrayList.Add((object) null);
        }
        else
          arrayList.Add(obj);
      }
      foreach (object obj in arrayList)
      {
        if (map.ContainsKey(obj))
          ++map[obj].Count;
        else
          map[obj] = strategy.CreateCluster(obj);
      }
    }

    protected virtual void SortClusters(IClusteringStrategy strategy, List<ICluster> clusters)
    {
      clusters.Sort();
    }

    protected virtual ToolStripMenuItem CreateFilteringMenuItem(
      OLVColumn column,
      List<ICluster> clusters)
    {
      ToolStripCheckedListBox checkedList = new ToolStripCheckedListBox();
      checkedList.Tag = (object) column;
      foreach (ICluster cluster in clusters)
        checkedList.AddItem((object) cluster, column.ValuesChosenForFiltering.Contains(cluster.ClusterKey));
      if (!string.IsNullOrEmpty(FilterMenuBuilder.SELECT_ALL_LABEL))
      {
        int count = checkedList.CheckedItems.Count;
        if (count == 0)
          checkedList.AddItem((object) FilterMenuBuilder.SELECT_ALL_LABEL, CheckState.Unchecked);
        else
          checkedList.AddItem((object) FilterMenuBuilder.SELECT_ALL_LABEL, count == clusters.Count ? CheckState.Checked : CheckState.Indeterminate);
      }
      checkedList.ItemCheck += new ItemCheckEventHandler(this.HandleItemCheckedWrapped);
      ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(FilterMenuBuilder.CLEAR_ALL_FILTERS_LABEL, (Image) FilterMenuBuilder.ClearFilteringImage, (EventHandler) ((sender, args) => this.ClearAllFilters(column)));
      ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(FilterMenuBuilder.APPLY_LABEL, (Image) FilterMenuBuilder.FilteringImage, (EventHandler) ((sender, args) => this.EnactFilter(checkedList, column)));
      return new ToolStripMenuItem(FilterMenuBuilder.FILTERING_LABEL, (Image) null, new ToolStripItem[4]
      {
        (ToolStripItem) toolStripMenuItem1,
        (ToolStripItem) new ToolStripSeparator(),
        (ToolStripItem) checkedList,
        (ToolStripItem) toolStripMenuItem2
      });
    }

    private void HandleItemCheckedWrapped(object sender, ItemCheckEventArgs e)
    {
      if (this.alreadyInHandleItemChecked)
        return;
      try
      {
        this.alreadyInHandleItemChecked = true;
        this.HandleItemChecked(sender, e);
      }
      finally
      {
        this.alreadyInHandleItemChecked = false;
      }
    }

    protected virtual void HandleItemChecked(object sender, ItemCheckEventArgs e)
    {
      if (!(sender is ToolStripCheckedListBox checkedList) || !(checkedList.Tag is OLVColumn tag) || !(tag.ListView is ObjectListView))
        return;
      int selectAllIndex = checkedList.Items.IndexOf((object) FilterMenuBuilder.SELECT_ALL_LABEL);
      if (selectAllIndex < 0)
        return;
      this.HandleSelectAllItem(e, checkedList, selectAllIndex);
    }

    protected virtual void HandleSelectAllItem(
      ItemCheckEventArgs e,
      ToolStripCheckedListBox checkedList,
      int selectAllIndex)
    {
      if (e.Index == selectAllIndex)
      {
        if (e.NewValue == CheckState.Checked)
          checkedList.CheckAll();
        if (e.NewValue != CheckState.Unchecked)
          return;
        checkedList.UncheckAll();
      }
      else
      {
        int count = checkedList.CheckedItems.Count;
        if (checkedList.GetItemCheckState(selectAllIndex) != CheckState.Unchecked)
          --count;
        if (e.NewValue != e.CurrentValue)
        {
          if (e.NewValue == CheckState.Checked)
            ++count;
          else
            --count;
        }
        if (count == 0)
          checkedList.SetItemState(selectAllIndex, CheckState.Unchecked);
        else if (count == checkedList.Items.Count - 1)
          checkedList.SetItemState(selectAllIndex, CheckState.Checked);
        else
          checkedList.SetItemState(selectAllIndex, CheckState.Indeterminate);
      }
    }

    protected virtual void ClearAllFilters(OLVColumn column)
    {
      if (!(column.ListView is ObjectListView listView) || listView.IsDisposed)
        return;
      listView.ResetColumnFiltering();
    }

    protected virtual void EnactFilter(ToolStripCheckedListBox checkedList, OLVColumn column)
    {
      if (!(column.ListView is ObjectListView listView) || listView.IsDisposed)
        return;
      ArrayList arrayList = new ArrayList();
      foreach (object checkedItem in checkedList.CheckedItems)
      {
        if (checkedItem is ICluster cluster)
          arrayList.Add(cluster.ClusterKey);
      }
      column.ValuesChosenForFiltering = (IList) arrayList;
      listView.UpdateColumnFiltering();
    }
  }
}
