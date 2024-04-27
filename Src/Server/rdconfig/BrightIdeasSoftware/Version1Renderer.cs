// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.Version1Renderer
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  [ToolboxItem(false)]
  internal class Version1Renderer : AbstractRenderer
  {
    public RenderDelegate RenderDelegate;

    public Version1Renderer(RenderDelegate renderDelegate) => this.RenderDelegate = renderDelegate;

    public override bool RenderSubItem(
      DrawListViewSubItemEventArgs e,
      Graphics g,
      Rectangle cellBounds,
      object rowObject)
    {
      return this.RenderDelegate == null ? base.RenderSubItem(e, g, cellBounds, rowObject) : this.RenderDelegate((EventArgs) e, g, cellBounds, rowObject);
    }
  }
}
