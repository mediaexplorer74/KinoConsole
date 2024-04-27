// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IntUpDown
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  [ToolboxItem(false)]
  public class IntUpDown : NumericUpDown
  {
    public IntUpDown()
    {
      this.DecimalPlaces = 0;
      this.Minimum = -9999999M;
      this.Maximum = 9999999M;
    }

    public int Value
    {
      get => Decimal.ToInt32(base.Value);
      set => this.Value = (int)(new Decimal(value));
    }
  }
}
