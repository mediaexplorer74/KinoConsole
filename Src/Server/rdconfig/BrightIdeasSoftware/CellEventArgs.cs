// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.CellEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class CellEventArgs : EventArgs
  {
    private ObjectListView listView;
    private object model;
    private int rowIndex = -1;
    private int columnIndex = -1;
    private OLVColumn column;
    private Point location;
    private Keys modifierKeys;
    private OLVListItem item;
    private OLVListSubItem subItem;
    private OlvListViewHitTestInfo hitTest;
    public bool Handled;

    public ObjectListView ListView
    {
      get => this.listView;
      internal set => this.listView = value;
    }

    public object Model
    {
      get => this.model;
      internal set => this.model = value;
    }

    public int RowIndex
    {
      get => this.rowIndex;
      internal set => this.rowIndex = value;
    }

    public int ColumnIndex
    {
      get => this.columnIndex;
      internal set => this.columnIndex = value;
    }

    public OLVColumn Column
    {
      get => this.column;
      internal set => this.column = value;
    }

    public Point Location
    {
      get => this.location;
      internal set => this.location = value;
    }

    public Keys ModifierKeys
    {
      get => this.modifierKeys;
      internal set => this.modifierKeys = value;
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

    public OlvListViewHitTestInfo HitTest
    {
      get => this.hitTest;
      internal set => this.hitTest = value;
    }
  }
}
