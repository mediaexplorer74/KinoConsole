// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.HotItemChangedEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class HotItemChangedEventArgs : EventArgs
  {
    private bool handled;
    private HitTestLocation newHotCellHitLocation;
    private HitTestLocationEx hotCellHitLocationEx;
    private int newHotColumnIndex;
    private int newHotRowIndex;
    private OLVGroup hotGroup;
    private HitTestLocation oldHotCellHitLocation;
    private HitTestLocationEx oldHotCellHitLocationEx;
    private int oldHotColumnIndex;
    private int oldHotRowIndex;
    private OLVGroup oldHotGroup;

    public bool Handled
    {
      get => this.handled;
      set => this.handled = value;
    }

    public HitTestLocation HotCellHitLocation
    {
      get => this.newHotCellHitLocation;
      internal set => this.newHotCellHitLocation = value;
    }

    public virtual HitTestLocationEx HotCellHitLocationEx
    {
      get => this.hotCellHitLocationEx;
      internal set => this.hotCellHitLocationEx = value;
    }

    public int HotColumnIndex
    {
      get => this.newHotColumnIndex;
      internal set => this.newHotColumnIndex = value;
    }

    public int HotRowIndex
    {
      get => this.newHotRowIndex;
      internal set => this.newHotRowIndex = value;
    }

    public OLVGroup HotGroup
    {
      get => this.hotGroup;
      internal set => this.hotGroup = value;
    }

    public HitTestLocation OldHotCellHitLocation
    {
      get => this.oldHotCellHitLocation;
      internal set => this.oldHotCellHitLocation = value;
    }

    public virtual HitTestLocationEx OldHotCellHitLocationEx
    {
      get => this.oldHotCellHitLocationEx;
      internal set => this.oldHotCellHitLocationEx = value;
    }

    public int OldHotColumnIndex
    {
      get => this.oldHotColumnIndex;
      internal set => this.oldHotColumnIndex = value;
    }

    public int OldHotRowIndex
    {
      get => this.oldHotRowIndex;
      internal set => this.oldHotRowIndex = value;
    }

    public OLVGroup OldHotGroup
    {
      get => this.oldHotGroup;
      internal set => this.oldHotGroup = value;
    }

    public override string ToString()
    {
      return string.Format("NewHotCellHitLocation: {0}, HotCellHitLocationEx: {1}, NewHotColumnIndex: {2}, NewHotRowIndex: {3}, HotGroup: {4}", (object) this.newHotCellHitLocation, (object) this.hotCellHitLocationEx, (object) this.newHotColumnIndex, (object) this.newHotRowIndex, (object) this.hotGroup);
    }
  }
}
