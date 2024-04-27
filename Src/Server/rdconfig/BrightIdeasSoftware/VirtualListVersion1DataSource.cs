// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.VirtualListVersion1DataSource
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe


namespace BrightIdeasSoftware
{
  public class VirtualListVersion1DataSource : AbstractVirtualListDataSource
  {
    private RowGetterDelegate rowGetter;

    public VirtualListVersion1DataSource(VirtualObjectListView listView)
      : base(listView)
    {
    }

    public RowGetterDelegate RowGetter
    {
      get => this.rowGetter;
      set => this.rowGetter = value;
    }

    public override object GetNthObject(int n)
    {
      return this.RowGetter == null ? (object) null : this.RowGetter(n);
    }

    public override int SearchText(string value, int first, int last, OLVColumn column)
    {
      return AbstractVirtualListDataSource.DefaultSearchText(value, first, last, column, (IVirtualListDataSource) this);
    }
  }
}
