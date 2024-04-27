// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.AfterSearchingEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class AfterSearchingEventArgs : EventArgs
  {
    private string stringToFind;
    public bool Handled;
    private int indexSelected;

    public AfterSearchingEventArgs(string stringToFind, int indexSelected)
    {
      this.stringToFind = stringToFind;
      this.indexSelected = indexSelected;
    }

    public string StringToFind => this.stringToFind;

    public int IndexSelected => this.indexSelected;
  }
}
