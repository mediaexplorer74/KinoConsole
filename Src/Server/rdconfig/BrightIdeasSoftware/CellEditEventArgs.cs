// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.CellEditEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class CellEditEventArgs : EventArgs
  {
    public bool Cancel;
    public Control Control;
    private OLVColumn column;
    private object rowObject;
    private OLVListItem listViewItem;
    private object newValue;
    private int subItemIndex;
    private object value;
    private Rectangle cellBounds;

    public CellEditEventArgs(
      OLVColumn column,
      Control control,
      Rectangle r,
      OLVListItem item,
      int subItemIndex)
    {
      this.Control = control;
      this.column = column;
      this.cellBounds = r;
      this.listViewItem = item;
      this.rowObject = item.RowObject;
      this.subItemIndex = subItemIndex;
      this.value = column.GetValue(item.RowObject);
    }

    public OLVColumn Column => this.column;

    public object RowObject => this.rowObject;

    public OLVListItem ListViewItem => this.listViewItem;

    public object NewValue
    {
      get => this.newValue;
      set => this.newValue = value;
    }

    public int SubItemIndex => this.subItemIndex;

    public object Value => this.value;

    public Rectangle CellBounds => this.cellBounds;
  }
}
