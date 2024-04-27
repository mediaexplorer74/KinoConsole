// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FormatCellEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe


namespace BrightIdeasSoftware
{
  public class FormatCellEventArgs : FormatRowEventArgs
  {
    private int columnIndex = -1;
    private OLVColumn column;
    private OLVListSubItem subItem;

    public int ColumnIndex
    {
      get => this.columnIndex;
      internal set => this.columnIndex = value;
    }

    public OLVColumn Column
    {
      get => this.column;
      internal set => this.column = value;
    }

    public OLVListSubItem SubItem
    {
      get => this.subItem;
      internal set => this.subItem = value;
    }

    public object CellValue => this.SubItem != null ? this.SubItem.ModelValue : (object) null;
  }
}
