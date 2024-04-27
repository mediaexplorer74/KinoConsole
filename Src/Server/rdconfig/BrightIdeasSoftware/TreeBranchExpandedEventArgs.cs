// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.TreeBranchExpandedEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class TreeBranchExpandedEventArgs : EventArgs
  {
    private object model;
    private OLVListItem item;

    public TreeBranchExpandedEventArgs(object model, OLVListItem item)
    {
      this.Model = model;
      this.Item = item;
    }

    public object Model
    {
      get => this.model;
      private set => this.model = value;
    }

    public OLVListItem Item
    {
      get => this.item;
      private set => this.item = value;
    }
  }
}
