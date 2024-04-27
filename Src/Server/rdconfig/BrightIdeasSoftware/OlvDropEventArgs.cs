// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.OlvDropEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class OlvDropEventArgs : EventArgs
  {
    private DragEventArgs dragEventArgs;
    private object dataObject;
    private SimpleDropSink dropSink;
    private int dropTargetIndex = -1;
    private DropTargetLocation dropTargetLocation;
    private int dropTargetSubItemIndex = -1;
    private DragDropEffects effect;
    private bool handled;
    private string infoMessage;
    private ObjectListView listView;
    private Point mouseLocation;

    public DragEventArgs DragEventArgs
    {
      get => this.dragEventArgs;
      internal set => this.dragEventArgs = value;
    }

    public object DataObject
    {
      get => this.dataObject;
      internal set => this.dataObject = value;
    }

    public SimpleDropSink DropSink
    {
      get => this.dropSink;
      internal set => this.dropSink = value;
    }

    public int DropTargetIndex
    {
      get => this.dropTargetIndex;
      set => this.dropTargetIndex = value;
    }

    public DropTargetLocation DropTargetLocation
    {
      get => this.dropTargetLocation;
      set => this.dropTargetLocation = value;
    }

    public int DropTargetSubItemIndex
    {
      get => this.dropTargetSubItemIndex;
      set => this.dropTargetSubItemIndex = value;
    }

    public OLVListItem DropTargetItem
    {
      get => this.ListView.GetItem(this.DropTargetIndex);
      set
      {
        if (value == null)
          this.DropTargetIndex = -1;
        else
          this.DropTargetIndex = value.Index;
      }
    }

    public DragDropEffects Effect
    {
      get => this.effect;
      set => this.effect = value;
    }

    public bool Handled
    {
      get => this.handled;
      set => this.handled = value;
    }

    public string InfoMessage
    {
      get => this.infoMessage;
      set => this.infoMessage = value;
    }

    public ObjectListView ListView
    {
      get => this.listView;
      internal set => this.listView = value;
    }

    public Point MouseLocation
    {
      get => this.mouseLocation;
      internal set => this.mouseLocation = value;
    }

    public DragDropEffects StandardDropActionFromKeys
    {
      get => this.DropSink.CalculateStandardDropActionFromKeys();
    }
  }
}
