// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FastObjectListView
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class FastObjectListView : VirtualObjectListView
  {
    public FastObjectListView()
    {
      this.VirtualListDataSource = (IVirtualListDataSource) new FastObjectListDataSource(this);
      this.GroupingStrategy = (IVirtualGroups) new FastListGroupingStrategy();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override IEnumerable FilteredObjects
    {
      get
      {
        return (IEnumerable) ((FastObjectListDataSource) this.VirtualListDataSource).FilteredObjectList;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IEnumerable Objects
    {
      get => (IEnumerable) ((FastObjectListDataSource) this.VirtualListDataSource).ObjectList;
      set => base.Objects = value;
    }

    public override void Unsort()
    {
      this.ShowGroups = false;
      this.PrimarySortColumn = (OLVColumn) null;
      this.PrimarySortOrder = SortOrder.None;
      this.SetObjects(this.Objects);
    }
  }
}
