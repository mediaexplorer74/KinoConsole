// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.ModelDropEventArgs
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;


namespace BrightIdeasSoftware
{
  public class ModelDropEventArgs : OlvDropEventArgs
  {
    private IList dragModels;
    private ArrayList toBeRefreshed = new ArrayList();
    private ObjectListView sourceListView;
    private object targetModel;

    public IList SourceModels
    {
      get => this.dragModels;
      internal set
      {
        this.dragModels = value;
        if (!(this.SourceListView is TreeListView sourceListView))
          return;
        foreach (object sourceModel in (IEnumerable) this.SourceModels)
        {
          object parent = sourceListView.GetParent(sourceModel);
          if (!this.toBeRefreshed.Contains(parent))
            this.toBeRefreshed.Add(parent);
        }
      }
    }

    public ObjectListView SourceListView
    {
      get => this.sourceListView;
      internal set => this.sourceListView = value;
    }

    public object TargetModel
    {
      get => this.targetModel;
      internal set => this.targetModel = value;
    }

    public void RefreshObjects()
    {
      this.toBeRefreshed.AddRange((ICollection) this.SourceModels);
      if (!(this.SourceListView is TreeListView sourceListView))
        this.SourceListView.RefreshObjects((IList) this.toBeRefreshed);
      else
        sourceListView.RebuildAll(true);
      if (!(this.ListView is TreeListView listView))
        this.ListView.RefreshObject(this.TargetModel);
      else
        listView.RebuildAll(true);
    }
  }
}
