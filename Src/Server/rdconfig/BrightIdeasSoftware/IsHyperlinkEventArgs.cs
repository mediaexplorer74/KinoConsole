// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IsHyperlinkEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class IsHyperlinkEventArgs : EventArgs
  {
    private ObjectListView listView;
    private object model;
    private OLVColumn column;
    private string text;
    public string Url;

    public ObjectListView ListView
    {
      get => this.listView;
      internal set => this.listView = value;
    }

    public object Model
    {
      get => this.model;
      internal set => this.model = value;
    }

    public OLVColumn Column
    {
      get => this.column;
      internal set => this.column = value;
    }

    public string Text
    {
      get => this.text;
      internal set => this.text = value;
    }
  }
}
