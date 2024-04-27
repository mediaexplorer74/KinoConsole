// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.AfterSortingEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class AfterSortingEventArgs : EventArgs
  {
    private OLVColumn columnToGroupBy;
    private SortOrder groupByOrder;
    private OLVColumn columnToSort;
    private SortOrder sortOrder;
    private OLVColumn secondaryColumnToSort;
    private SortOrder secondarySortOrder;

    public AfterSortingEventArgs(
      OLVColumn groupColumn,
      SortOrder groupOrder,
      OLVColumn column,
      SortOrder order,
      OLVColumn column2,
      SortOrder order2)
    {
      this.columnToGroupBy = groupColumn;
      this.groupByOrder = groupOrder;
      this.columnToSort = column;
      this.sortOrder = order;
      this.secondaryColumnToSort = column2;
      this.secondarySortOrder = order2;
    }

    public AfterSortingEventArgs(BeforeSortingEventArgs args)
    {
      this.columnToGroupBy = args.ColumnToGroupBy;
      this.groupByOrder = args.GroupByOrder;
      this.columnToSort = args.ColumnToSort;
      this.sortOrder = args.SortOrder;
      this.secondaryColumnToSort = args.SecondaryColumnToSort;
      this.secondarySortOrder = args.SecondarySortOrder;
    }

    public OLVColumn ColumnToGroupBy => this.columnToGroupBy;

    public SortOrder GroupByOrder => this.groupByOrder;

    public OLVColumn ColumnToSort => this.columnToSort;

    public SortOrder SortOrder => this.sortOrder;

    public OLVColumn SecondaryColumnToSort => this.secondaryColumnToSort;

    public SortOrder SecondarySortOrder => this.secondarySortOrder;
  }
}
