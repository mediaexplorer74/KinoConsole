// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.OLVDataObject
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class OLVDataObject : DataObject
  {
    private readonly bool includeHiddenColumns;
    private readonly bool includeColumnHeaders;
    private readonly ObjectListView objectListView;
    private readonly IList modelObjects;

    public OLVDataObject(ObjectListView olv)
      : this(olv, olv.SelectedObjects)
    {
    }

    public OLVDataObject(ObjectListView olv, IList modelObjects)
    {
      this.objectListView = olv;
      this.modelObjects = modelObjects;
      this.includeHiddenColumns = olv.IncludeHiddenColumnsInDataTransfer;
      this.includeColumnHeaders = olv.IncludeColumnHeadersInCopy;
      this.CreateTextFormats();
    }

    public bool IncludeHiddenColumns => this.includeHiddenColumns;

    public bool IncludeColumnHeaders => this.includeColumnHeaders;

    public ObjectListView ListView => this.objectListView;

    public IList ModelObjects => this.modelObjects;

    public void CreateTextFormats()
    {
      OLVExporter exporter = this.CreateExporter();
      this.SetData((object) exporter.ExportTo(OLVExporter.ExportFormat.TabSeparated));
      this.SetText(exporter.ExportTo(OLVExporter.ExportFormat.CSV), TextDataFormat.CommaSeparatedValue);
      this.SetText(this.ConvertToHtmlFragment(exporter.ExportTo(OLVExporter.ExportFormat.HTML)), TextDataFormat.Html);
    }

    protected OLVExporter CreateExporter()
    {
      return new OLVExporter(this.ListView)
      {
        IncludeColumnHeaders = this.IncludeColumnHeaders,
        IncludeHiddenColumns = this.IncludeHiddenColumns,
        ModelObjects = this.ModelObjects
      };
    }

    [Obsolete("Use OLVExporter directly instead", false)]
    public string CreateHtml() => this.CreateExporter().ExportTo(OLVExporter.ExportFormat.HTML);

    private string ConvertToHtmlFragment(string fragment)
    {
      int length = string.Format("Version:1.0\r\nStartHTML:{0,8}\r\nEndHTML:{1,8}\r\nStartFragment:{2,8}\r\nEndFragment:{3,8}\r\nStartSelection:{2,8}\r\nEndSelection:{3,8}\r\nSourceURL:{4}\r\n{5}", (object) 0, (object) 0, (object) 0, (object) 0, (object) "http://www.codeproject.com/Articles/16009/A-Much-Easier-to-Use-ListView", (object) "").Length;
      string str = string.Format("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\"><HTML><HEAD></HEAD><BODY><!--StartFragment-->{0}<!--EndFragment--></BODY></HTML>", (object) fragment);
      int num1 = length + str.IndexOf(fragment);
      int num2 = num1 + fragment.Length;
      return string.Format("Version:1.0\r\nStartHTML:{0,8}\r\nEndHTML:{1,8}\r\nStartFragment:{2,8}\r\nEndFragment:{3,8}\r\nStartSelection:{2,8}\r\nEndSelection:{3,8}\r\nSourceURL:{4}\r\n{5}", (object) length, (object) (length + str.Length), (object) num1, (object) num2, (object) "http://www.codeproject.com/Articles/16009/A-Much-Easier-to-Use-ListView", (object) str);
    }
  }
}
