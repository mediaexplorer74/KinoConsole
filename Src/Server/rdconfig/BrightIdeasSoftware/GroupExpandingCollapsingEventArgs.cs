// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.GroupExpandingCollapsingEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class GroupExpandingCollapsingEventArgs : CancellableEventArgs
  {
    private readonly OLVGroup olvGroup;

    public GroupExpandingCollapsingEventArgs(OLVGroup group)
    {
      this.olvGroup = group != null ? group : throw new ArgumentNullException(nameof (group));
    }

    public OLVGroup Group => this.olvGroup;

    public bool IsExpanding => this.Group.Collapsed;
  }
}
