// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FilterEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;


namespace BrightIdeasSoftware
{
  public class FilterEventArgs : EventArgs
  {
    public IEnumerable Objects;
    public IEnumerable FilteredObjects;

    public FilterEventArgs(IEnumerable objects) => this.Objects = objects;
  }
}
