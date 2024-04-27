// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.SimpleDragSource
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class SimpleDragSource : IDragSource
  {
    private bool refreshAfterDrop;

    public SimpleDragSource()
    {
    }

    public SimpleDragSource(bool refreshAfterDrop) => this.RefreshAfterDrop = refreshAfterDrop;

    public bool RefreshAfterDrop
    {
      get => this.refreshAfterDrop;
      set => this.refreshAfterDrop = value;
    }

    public virtual object StartDrag(ObjectListView olv, MouseButtons button, OLVListItem item)
    {
      return button != MouseButtons.Left ? (object) null : this.CreateDataObject(olv);
    }

    public virtual DragDropEffects GetAllowedEffects(object data)
    {
      return DragDropEffects.All | DragDropEffects.Link;
    }

    public virtual void EndDrag(object dragObject, DragDropEffects effect)
    {
      if (!(dragObject is OLVDataObject olvDataObject) || !this.RefreshAfterDrop)
        return;
      olvDataObject.ListView.RefreshObjects(olvDataObject.ModelObjects);
    }

    protected virtual object CreateDataObject(ObjectListView olv)
    {
      return (object) new OLVDataObject(olv);
    }
  }
}
