// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.AbstractDropSink
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class AbstractDropSink : IDropSink
  {
    private ObjectListView listView;

    public virtual ObjectListView ListView
    {
      get => this.listView;
      set => this.listView = value;
    }

    public virtual void DrawFeedback(Graphics g, Rectangle bounds)
    {
    }

    public virtual void Drop(DragEventArgs args) => this.Cleanup();

    public virtual void Enter(DragEventArgs args)
    {
    }

    public virtual void Leave() => this.Cleanup();

    public virtual void Over(DragEventArgs args)
    {
    }

    public virtual void GiveFeedback(GiveFeedbackEventArgs args) => args.UseDefaultCursors = true;

    public virtual void QueryContinue(QueryContinueDragEventArgs args)
    {
    }

    protected virtual void Cleanup()
    {
    }
  }
}
