// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FormatRowEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class FormatRowEventArgs : EventArgs
  {
    private ObjectListView listView;
    private OLVListItem item;
    private int rowIndex = -1;
    private int displayIndex = -1;
    private bool useCellFormatEvents;

    public ObjectListView ListView
    {
      get => this.listView;
      internal set => this.listView = value;
    }

    public OLVListItem Item
    {
      get => this.item;
      internal set => this.item = value;
    }

    public object Model => this.Item.RowObject;

    public int RowIndex
    {
      get => this.rowIndex;
      internal set => this.rowIndex = value;
    }

    public int DisplayIndex
    {
      get => this.displayIndex;
      internal set => this.displayIndex = value;
    }

    public bool UseCellFormatEvents
    {
      get => this.useCellFormatEvents;
      set => this.useCellFormatEvents = value;
    }
  }
}
