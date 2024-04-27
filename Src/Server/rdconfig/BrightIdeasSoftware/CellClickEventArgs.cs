// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.CellClickEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe


namespace BrightIdeasSoftware
{
  public class CellClickEventArgs : CellEventArgs
  {
    private int clickCount;

    public int ClickCount
    {
      get => this.clickCount;
      set => this.clickCount = value;
    }
  }
}
