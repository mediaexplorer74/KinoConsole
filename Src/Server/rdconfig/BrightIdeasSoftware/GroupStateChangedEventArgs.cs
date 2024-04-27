// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.GroupStateChangedEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class GroupStateChangedEventArgs : EventArgs
  {
    private readonly OLVGroup group;
    private readonly GroupState oldState;
    private readonly GroupState newState;

    public GroupStateChangedEventArgs(OLVGroup group, GroupState oldState, GroupState newState)
    {
      this.group = group;
      this.oldState = oldState;
      this.newState = newState;
    }

    public bool Collapsed
    {
      get
      {
        return (this.oldState & GroupState.LVGS_COLLAPSED) != GroupState.LVGS_COLLAPSED && (this.newState & GroupState.LVGS_COLLAPSED) == GroupState.LVGS_COLLAPSED;
      }
    }

    public bool Focused
    {
      get
      {
        return (this.oldState & GroupState.LVGS_FOCUSED) != GroupState.LVGS_FOCUSED && (this.newState & GroupState.LVGS_FOCUSED) == GroupState.LVGS_FOCUSED;
      }
    }

    public bool Selected
    {
      get
      {
        return (this.oldState & GroupState.LVGS_SELECTED) != GroupState.LVGS_SELECTED && (this.newState & GroupState.LVGS_SELECTED) == GroupState.LVGS_SELECTED;
      }
    }

    public bool Uncollapsed
    {
      get
      {
        return (this.oldState & GroupState.LVGS_COLLAPSED) == GroupState.LVGS_COLLAPSED && (this.newState & GroupState.LVGS_COLLAPSED) != GroupState.LVGS_COLLAPSED;
      }
    }

    public bool Unfocused
    {
      get
      {
        return (this.oldState & GroupState.LVGS_FOCUSED) == GroupState.LVGS_FOCUSED && (this.newState & GroupState.LVGS_FOCUSED) != GroupState.LVGS_FOCUSED;
      }
    }

    public bool Unselected
    {
      get
      {
        return (this.oldState & GroupState.LVGS_SELECTED) == GroupState.LVGS_SELECTED && (this.newState & GroupState.LVGS_SELECTED) != GroupState.LVGS_SELECTED;
      }
    }

    public OLVGroup Group => this.group;

    public GroupState OldState => this.oldState;

    public GroupState NewState => this.newState;
  }
}
