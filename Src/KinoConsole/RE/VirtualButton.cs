// Decompiled with JetBrains decompiler
// Type: KinoConsole.VirtualButton
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System;

namespace KinoConsole
{
  public class VirtualButton
  {
    public string name;

    public Uri imageUri { get; set; }

    public VirtualButton(string buttonName)
    {
      this.name = buttonName;
      this.imageUri = new Uri("/Assets/Buttons/" + buttonName + ".png", UriKind.Relative);
    }
  }
}
