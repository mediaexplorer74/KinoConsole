// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.AbstractVirtualGroups
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections.Generic;


namespace BrightIdeasSoftware
{
  public class AbstractVirtualGroups : IVirtualGroups
  {
    public virtual IList<OLVGroup> GetGroups(GroupingParameters parameters)
    {
      return (IList<OLVGroup>) new List<OLVGroup>();
    }

    public virtual int GetGroupMember(OLVGroup group, int indexWithinGroup) => -1;

    public virtual int GetGroup(int itemIndex) => -1;

    public virtual int GetIndexWithinGroup(OLVGroup group, int itemIndex) => -1;

    public virtual void CacheHint(
      int fromGroupIndex,
      int fromIndex,
      int toGroupIndex,
      int toIndex)
    {
    }
  }
}
