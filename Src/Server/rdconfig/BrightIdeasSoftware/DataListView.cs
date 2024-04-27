// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.DataListView
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;


namespace BrightIdeasSoftware
{
  public class DataListView : ObjectListView
  {
    private DataSourceAdapter adapter;

    public DataListView() => this.Adapter = new DataSourceAdapter((ObjectListView) this);

    [Category("Data")]
    [DefaultValue(true)]
    [Description("Should the control automatically generate columns from the DataSource")]
    public bool AutoGenerateColumns
    {
      get => this.Adapter.AutoGenerateColumns;
      set => this.Adapter.AutoGenerateColumns = value;
    }

    [Category("Data")]
    [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
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
      get => this.adapter;
      set => this.adapter = value;
    }

    public override void AddObjects(ICollection modelObjects)
    {
    }

    public override void RemoveObjects(ICollection modelObjects)
    {
    }

    protected override void OnParentBindingContextChanged(EventArgs e)
    {
      base.OnParentBindingContextChanged(e);
    }
  }
}
