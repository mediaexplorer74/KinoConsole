// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.AbstractDragSource
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class AbstractDragSource : IDragSource
  {
    public virtual object StartDrag(ObjectListView olv, MouseButtons button, OLVListItem item)
    {
      return (object) null;
    }

    public virtual DragDropEffects GetAllowedEffects(object data) => DragDropEffects.None;

    public virtual void EndDrag(object dragObject, DragDropEffects effect)
    {
    }
  }
}
