// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ItemsChangingEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;


namespace BrightIdeasSoftware
{
  public class ItemsChangingEventArgs : CancellableEventArgs
  {
    private IEnumerable oldObjects;
    public IEnumerable NewObjects;

    public ItemsChangingEventArgs(IEnumerable oldObjects, IEnumerable newObjects)
    {
      this.oldObjects = oldObjects;
      this.NewObjects = newObjects;
    }

    public IEnumerable OldObjects => this.oldObjects;
  }
}
