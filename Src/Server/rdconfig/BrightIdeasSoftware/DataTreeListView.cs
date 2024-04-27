// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.DataTreeListView
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;


namespace BrightIdeasSoftware
{
  public class DataTreeListView : TreeListView
  {
    private TreeDataSourceAdapter adapter;

    [Category("Data")]
    [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
    public virtual object DataSource
    {
      get => this.Adapter.DataSource;
      set => this.Adapter.DataSource = value;
    }

    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", typeof (UITypeEditor))]
    [Category("Data")]
    public virtual string DataMember
    {
      get => this.Adapter.DataMember;
      set => this.Adapter.DataMember = value;
    }

    [Description("The name of the property/column that holds the key of a row")]
    [DefaultValue(null)]
    [Category("Data")]
    public virtual string KeyAspectName
    {
      get => this.Adapter.KeyAspectName;
      set => this.Adapter.KeyAspectName = value;
    }

    [Category("Data")]
    [Description("The name of the property/column that holds the key of the parent of a row")]
    [DefaultValue(null)]
    public virtual string ParentKeyAspectName
    {
      get => this.Adapter.ParentKeyAspectName;
      set => this.Adapter.ParentKeyAspectName = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual object RootKeyValue
    {
      get => this.Adapter.RootKeyValue;
      set => this.Adapter.RootKeyValue = value;
    }

    [DefaultValue(null)]
    [Description("The parent id value that identifies a row as a root object")]
    [Category("Data")]
    public virtual string RootKeyValueString
    {
      get => Convert.ToString(this.Adapter.RootKeyValue);
      set => this.Adapter.RootKeyValue = (object) value;
    }

    [DefaultValue(true)]
    [Description("Should the keys columns (id and parent id) be shown to the user?")]
    [Category("Data")]
    public virtual bool ShowKeyColumns
    {
      get => this.Adapter.ShowKeyColumns;
      set => this.Adapter.ShowKeyColumns = value;
    }

    protected TreeDataSourceAdapter Adapter
    {
      get
      {
        if (this.adapter == null)
          this.adapter = new TreeDataSourceAdapter(this);
        return this.adapter;
      }
      set => this.adapter = value;
    }
  }
}
