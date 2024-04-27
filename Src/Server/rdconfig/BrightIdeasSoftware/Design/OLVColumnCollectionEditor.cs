// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.Design.OLVColumnCollectionEditor
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace BrightIdeasSoftware.Design
{
  public class OLVColumnCollectionEditor : CollectionEditor
  {
    public OLVColumnCollectionEditor(System.Type t)
      : base(t)
    {
    }

    protected override System.Type CreateCollectionItemType() => typeof (OLVColumn);

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      if (provider == null)
        throw new ArgumentNullException(nameof (provider));
      ObjectListView instance = context.Instance as ObjectListView;
      base.EditValue(context, provider, (object) instance.AllColumns);
      List<OLVColumn> filteredColumns = instance.GetFilteredColumns(View.Details);
      instance.Columns.Clear();
      instance.Columns.AddRange((ColumnHeader[]) filteredColumns.ToArray());
      return (object) instance.Columns;
    }

    protected override string GetDisplayText(object value)
    {
      return !(value is OLVColumn olvColumn) || string.IsNullOrEmpty(olvColumn.AspectName) ? base.GetDisplayText(value) : string.Format("{0} ({1})", (object) base.GetDisplayText(value), (object) olvColumn.AspectName);
    }
  }
}
