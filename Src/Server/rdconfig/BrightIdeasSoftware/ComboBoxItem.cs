// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ComboBoxItem
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe


namespace BrightIdeasSoftware
{
  public class ComboBoxItem
  {
    private readonly string description;
    private readonly object key;

    public ComboBoxItem(object key, string description)
    {
      this.key = key;
      this.description = description;
    }

    public object Key => this.key;

    public override string ToString() => this.description;
  }
}
