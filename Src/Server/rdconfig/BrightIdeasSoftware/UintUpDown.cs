// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.UintUpDown
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  [ToolboxItem(false)]
  internal class UintUpDown : NumericUpDown
  {
    public UintUpDown()
    {
      this.DecimalPlaces = 0;
      this.Minimum = 0M;
      this.Maximum = 9999999M;
    }

    public uint Value
    {
      get => Decimal.ToUInt32(base.Value);
      set => this.Value = (uint)new Decimal(value);
    }
  }
}
