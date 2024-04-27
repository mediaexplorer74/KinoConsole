// Decompiled with JetBrains decompiler
// Type: TAFactory.IconPack.IconListViewItem
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;
using System.Windows.Forms;


namespace TAFactory.IconPack
{
  public class IconListViewItem : ListViewItem
  {
    private Icon _icon;

    public Icon Icon
    {
      get => this._icon;
      set => this._icon = value;
    }
  }
}
