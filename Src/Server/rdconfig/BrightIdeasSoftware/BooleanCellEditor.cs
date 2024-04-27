// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.BooleanCellEditor
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  [ToolboxItem(false)]
  public class BooleanCellEditor : ComboBox
  {
    public BooleanCellEditor()
    {
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.ValueMember = "Key";
      this.DataSource = (object) new ArrayList()
      {
        (object) new ComboBoxItem((object) false, "False"),
        (object) new ComboBoxItem((object) true, "True")
      };
    }
  }
}
