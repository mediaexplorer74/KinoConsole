// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.HyperlinkEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class HyperlinkEventArgs : EventArgs
  {
    private ObjectListView listView;
    private object model;
    private int rowIndex = -1;
    private int columnIndex = -1;
    private OLVColumn column;
    private OLVListItem item;
    private OLVListSubItem subItem;
    private string url;
    private bool handled;

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

    public string Url
    {
      get => this.url;
      internal set => this.url = value;
    }

    public bool Handled
    {
      get => this.handled;
      set => this.handled = value;
    }
  }
}
