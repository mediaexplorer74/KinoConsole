// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IOwnerDataCallback
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Runtime.InteropServices;


namespace BrightIdeasSoftware
{
  [Guid("44C09D56-8D3B-419D-A462-7B956B105B47")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IOwnerDataCallback
  {
    void GetItemPosition(int i, out NativeMethods.POINT pt);

    void SetItemPosition(int t, NativeMethods.POINT pt);

    void GetItemInGroup(int groupIndex, int n, out int itemIndex);

    void GetItemGroup(int itemIndex, int occurrenceCount, out int groupIndex);

    void GetItemGroupCount(int itemIndex, out int occurrenceCount);

    void OnCacheHint(NativeMethods.LVITEMINDEX i, NativeMethods.LVITEMINDEX j);
  }
}
