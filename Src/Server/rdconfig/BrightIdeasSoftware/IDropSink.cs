// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IDropSink
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public interface IDropSink
  {
    ObjectListView ListView { get; set; }

    void DrawFeedback(Graphics g, Rectangle bounds);

    void Drop(DragEventArgs args);

    void Enter(DragEventArgs args);

    void GiveFeedback(GiveFeedbackEventArgs args);

    void Leave();

    void Over(DragEventArgs args);

    void QueryContinue(QueryContinueDragEventArgs args);
  }
}
