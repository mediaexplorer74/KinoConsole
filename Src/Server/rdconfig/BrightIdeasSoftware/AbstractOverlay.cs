// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.AbstractOverlay
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Drawing;


namespace BrightIdeasSoftware
{
  public class AbstractOverlay : ITransparentOverlay, IOverlay
  {
    private int transparency = 128;

    public virtual void Draw(ObjectListView olv, Graphics g, Rectangle r)
    {
    }

    [Category("ObjectListView")]
    [NotifyParentProperty(true)]
    [Description("How transparent should this overlay be")]
    [DefaultValue(128)]
    public int Transparency
    {
      get => this.transparency;
      set => this.transparency = Math.Min((int) byte.MaxValue, Math.Max(0, value));
    }
  }
}
