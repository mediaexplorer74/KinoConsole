// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.SubItemCheckingEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class SubItemCheckingEventArgs : CancellableEventArgs
  {
    private OLVColumn column;
    private OLVListItem listViewItem;
    private CheckState currentValue;
    private CheckState newValue;
    private int subItemIndex;

    public SubItemCheckingEventArgs(
      OLVColumn column,
      OLVListItem item,
      int subItemIndex,
      CheckState currentValue,
      CheckState newValue)
    {
      this.column = column;
      this.listViewItem = item;
      this.subItemIndex = subItemIndex;
      this.currentValue = currentValue;
      this.newValue = newValue;
    }

    public OLVColumn Column => this.column;

    public object RowObject => this.listViewItem.RowObject;

    public OLVListItem ListViewItem => this.listViewItem;

    public CheckState CurrentValue => this.currentValue;

    public CheckState NewValue
    {
      get => this.newValue;
      set => this.newValue = value;
    }

    public int SubItemIndex => this.subItemIndex;
  }
}
