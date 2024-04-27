// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ItemsChangedEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class ItemsChangedEventArgs : EventArgs
  {
    private int oldObjectCount;
    private int newObjectCount;

    public ItemsChangedEventArgs()
    {
    }

    public ItemsChangedEventArgs(int oldObjectCount, int newObjectCount)
    {
      this.oldObjectCount = oldObjectCount;
      this.newObjectCount = newObjectCount;
    }

    public int OldObjectCount => this.oldObjectCount;

    public int NewObjectCount => this.newObjectCount;
  }
}
