// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.HyperlinkClickedEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe


namespace BrightIdeasSoftware
{
  public class HyperlinkClickedEventArgs : CellEventArgs
  {
    private string url;

    public string Url
    {
      get => this.url;
      set => this.url = value;
    }
  }
}
