﻿// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.OwnerDataCallbackImpl
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Runtime.InteropServices;


namespace BrightIdeasSoftware
{
  [Guid("6FC61F50-80E8-49b4-B200-3F38D3865ABD")]
  internal class OwnerDataCallbackImpl : IOwnerDataCallback
  {
    private VirtualObjectListView olv;

    public OwnerDataCallbackImpl(VirtualObjectListView olv) => this.olv = olv;

    public void GetItemPosition(int i, out NativeMethods.POINT pt)
    {
      throw new NotSupportedException();
    }

    public void SetItemPosition(int t, NativeMethods.POINT pt) => throw new NotSupportedException();

    public void GetItemInGroup(int groupIndex, int n, out int itemIndex)
    {
      itemIndex = this.olv.GroupingStrategy.GetGroupMember(this.olv.OLVGroups[groupIndex], n);
    }

    public void GetItemGroup(int itemIndex, int occurrenceCount, out int groupIndex)
    {
      groupIndex = this.olv.GroupingStrategy.GetGroup(itemIndex);
    }

    public void GetItemGroupCount(int itemIndex, out int occurrenceCount) => occurrenceCount = 1;

    public void OnCacheHint(NativeMethods.LVITEMINDEX from, NativeMethods.LVITEMINDEX to)
    {
      this.olv.GroupingStrategy.CacheHint(from.iGroup, from.iItem, to.iGroup, to.iItem);
    }
  }
}
