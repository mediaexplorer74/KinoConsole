// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.RearrangingDropSink
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class RearrangingDropSink : SimpleDropSink
  {
    public RearrangingDropSink()
    {
      this.CanDropBetween = true;
      this.CanDropOnBackground = true;
      this.CanDropOnItem = false;
    }

    public RearrangingDropSink(bool acceptDropsFromOtherLists)
      : this()
    {
      this.AcceptExternal = acceptDropsFromOtherLists;
    }

    protected override void OnModelCanDrop(ModelDropEventArgs args)
    {
      base.OnModelCanDrop(args);
      if (args.Handled)
        return;
      args.Effect = DragDropEffects.Move;
      if (!this.AcceptExternal && args.SourceListView != this.ListView)
      {
        args.Effect = DragDropEffects.None;
        args.DropTargetLocation = DropTargetLocation.None;
        args.InfoMessage = "This list doesn't accept drops from other lists";
      }
      if (args.DropTargetLocation != DropTargetLocation.Background || args.SourceListView != this.ListView)
        return;
      args.Effect = DragDropEffects.None;
      args.DropTargetLocation = DropTargetLocation.None;
    }

    protected override void OnModelDropped(ModelDropEventArgs args)
    {
      base.OnModelDropped(args);
      if (args.Handled)
        return;
      this.RearrangeModels(args);
    }

    public virtual void RearrangeModels(ModelDropEventArgs args)
    {
      switch (args.DropTargetLocation)
      {
        case DropTargetLocation.Background:
          this.ListView.AddObjects((ICollection) args.SourceModels);
          break;
        case DropTargetLocation.AboveItem:
          this.ListView.MoveObjects(args.DropTargetIndex, (ICollection) args.SourceModels);
          break;
        case DropTargetLocation.BelowItem:
          this.ListView.MoveObjects(args.DropTargetIndex + 1, (ICollection) args.SourceModels);
          break;
        default:
          return;
      }
      if (args.SourceListView == this.ListView)
        return;
      args.SourceListView.RemoveObjects((ICollection) args.SourceModels);
    }
  }
}
