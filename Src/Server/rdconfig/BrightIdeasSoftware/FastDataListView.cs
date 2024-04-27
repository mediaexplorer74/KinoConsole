// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FastDataListView
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.ComponentModel;
using System.Drawing.Design;


namespace BrightIdeasSoftware
{
  public class FastDataListView : FastObjectListView
  {
    private DataSourceAdapter adapter;

    [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
    [Category("Data")]
    public virtual object DataSource
    {
      get => this.Adapter.DataSource;
      set => this.Adapter.DataSource = value;
    }

    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", typeof (UITypeEditor))]
    [DefaultValue("")]
    public virtual string DataMember
    {
      get => this.Adapter.DataMember;
      set => this.Adapter.DataMember = value;
    }

    protected DataSourceAdapter Adapter
    {
      get
      {
        if (this.adapter == null)
          this.adapter = this.CreateDataSourceAdapter();
        return this.adapter;
      }
      set => this.adapter = value;
    }

    protected virtual DataSourceAdapter CreateDataSourceAdapter()
    {
      return new DataSourceAdapter((ObjectListView) this);
    }
  }
}
