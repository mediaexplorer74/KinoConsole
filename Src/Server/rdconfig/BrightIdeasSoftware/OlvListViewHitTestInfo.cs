// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.OlvListViewHitTestInfo
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class OlvListViewHitTestInfo
  {
    public HitTestLocation HitTestLocation;
    public HitTestLocationEx HitTestLocationEx;
    public OLVGroup Group;
    public object UserData;
    private OLVListItem item;
    private OLVListSubItem subItem;
    private ListViewHitTestLocations location;

    public OlvListViewHitTestInfo(
      OLVListItem olvListItem,
      OLVListSubItem subItem,
      int flags,
      OLVGroup group)
    {
      this.item = olvListItem;
      this.subItem = subItem;
      this.location = OlvListViewHitTestInfo.ConvertNativeFlagsToDotNetLocation(olvListItem, flags);
      this.HitTestLocationEx = (HitTestLocationEx) flags;
      this.Group = group;
      switch (this.location)
      {
        case ListViewHitTestLocations.Image:
          this.HitTestLocation = HitTestLocation.Image;
          break;
        case ListViewHitTestLocations.Label:
          this.HitTestLocation = HitTestLocation.Text;
          break;
        case ListViewHitTestLocations.StateImage:
          this.HitTestLocation = HitTestLocation.CheckBox;
          break;
        default:
          if ((this.HitTestLocationEx & HitTestLocationEx.LVHT_EX_GROUP_COLLAPSE) == HitTestLocationEx.LVHT_EX_GROUP_COLLAPSE)
          {
            this.HitTestLocation = HitTestLocation.GroupExpander;
            break;
          }
          if ((this.HitTestLocationEx & HitTestLocationEx.LVHT_EX_GROUP_MINUS_FOOTER_AND_BKGRD) != (HitTestLocationEx) 0)
          {
            this.HitTestLocation = HitTestLocation.Group;
            break;
          }
          this.HitTestLocation = HitTestLocation.Nothing;
          break;
      }
    }

    private static ListViewHitTestLocations ConvertNativeFlagsToDotNetLocation(
      OLVListItem hitItem,
      int flags)
    {
      return (8 & flags) == 8 ? (ListViewHitTestLocations) (247 & flags | (hitItem == null ? 256 : 512)) : (ListViewHitTestLocations) (flags & (int) ushort.MaxValue);
    }

    public OLVListItem Item
    {
      get => this.item;
      internal set => this.item = value;
    }

    public OLVListSubItem SubItem
    {
      get => this.subItem;
      internal set => this.subItem = value;
    }

    public ListViewHitTestLocations Location
    {
      get => this.location;
      internal set => this.location = value;
    }

    public ObjectListView ListView
    {
      get => this.Item != null ? (ObjectListView) this.Item.ListView : (ObjectListView) null;
    }

    public object RowObject => this.Item != null ? this.Item.RowObject : (object) null;

    public int RowIndex => this.Item != null ? this.Item.Index : -1;

    public int ColumnIndex
    {
      get
      {
        return this.Item == null || this.SubItem == null ? -1 : this.Item.SubItems.IndexOf((ListViewItem.ListViewSubItem) this.SubItem);
      }
    }

    public OLVColumn Column
    {
      get
      {
        int columnIndex = this.ColumnIndex;
        return columnIndex >= 0 ? this.ListView.GetColumn(columnIndex) : (OLVColumn) null;
      }
    }

    public override string ToString()
    {
      return string.Format("HitTestLocation: {0}, HitTestLocationEx: {1}, Item: {2}, SubItem: {3}, Location: {4}, Group: {5}", (object) this.HitTestLocation, (object) this.HitTestLocationEx, (object) this.item, (object) this.subItem, (object) this.location, (object) this.Group);
    }
  }
}
