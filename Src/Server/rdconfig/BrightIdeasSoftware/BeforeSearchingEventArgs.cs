// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.BeforeSearchingEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe


namespace BrightIdeasSoftware
{
  public class BeforeSearchingEventArgs : CancellableEventArgs
  {
    public string StringToFind;
    public int StartSearchFrom;

    public BeforeSearchingEventArgs(string stringToFind, int startSearchFrom)
    {
      this.StringToFind = stringToFind;
      this.StartSearchFrom = startSearchFrom;
    }
  }
}
