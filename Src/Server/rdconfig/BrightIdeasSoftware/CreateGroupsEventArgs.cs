// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.CreateGroupsEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections.Generic;


namespace BrightIdeasSoftware
{
  public class CreateGroupsEventArgs : EventArgs
  {
    private GroupingParameters parameters;
    private IList<OLVGroup> groups;
    private bool canceled;

    public CreateGroupsEventArgs(GroupingParameters parms) => this.parameters = parms;

    public GroupingParameters Parameters => this.parameters;

    public IList<OLVGroup> Groups
    {
      get => this.groups;
      set => this.groups = value;
    }

    public bool Canceled
    {
      get => this.canceled;
      set => this.canceled = value;
    }
  }
}
