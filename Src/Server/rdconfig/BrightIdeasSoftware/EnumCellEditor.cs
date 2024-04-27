// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.EnumCellEditor
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  [ToolboxItem(false)]
  public class EnumCellEditor : ComboBox
  {
    public EnumCellEditor(System.Type type)
    {
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.ValueMember = "Key";
      ArrayList arrayList = new ArrayList();
      foreach (object key in Enum.GetValues(type))
        arrayList.Add((object) new ComboBoxItem(key, Enum.GetName(type, key)));
      this.DataSource = (object) arrayList;
    }
  }
}
