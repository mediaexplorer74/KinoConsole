// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IVirtualGroups
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections.Generic;


namespace BrightIdeasSoftware
{
  public interface IVirtualGroups
  {
    IList<OLVGroup> GetGroups(GroupingParameters parameters);

    int GetGroupMember(OLVGroup group, int indexWithinGroup);

    int GetGroup(int itemIndex);

    int GetIndexWithinGroup(OLVGroup group, int itemIndex);

    void CacheHint(int fromGroupIndex, int fromIndex, int toGroupIndex, int toIndex);
  }
}
