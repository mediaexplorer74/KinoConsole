// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.OLVListSubItem
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  [Browsable(false)]
  public class OLVListSubItem : ListViewItem.ListViewSubItem
  {
    private Rectangle? cellPadding;
    private StringAlignment? cellVerticalAlignment;
    private object modelValue;
    private IList<IDecoration> decorations;
    private object imageSelector;
    private string url;
    internal ImageRenderer.AnimationState AnimationState;

    public OLVListSubItem()
    {
    }

    public OLVListSubItem(object modelValue, string text, object image)
    {
      this.ModelValue = modelValue;
      this.Text = text;
      this.ImageSelector = image;
    }

    public Rectangle? CellPadding
    {
      get => this.cellPadding;
      set => this.cellPadding = value;
    }

    public StringAlignment? CellVerticalAlignment
    {
      get => this.cellVerticalAlignment;
      set => this.cellVerticalAlignment = value;
    }

    public object ModelValue
    {
      get => this.modelValue;
      private set => this.modelValue = value;
    }

    public bool HasDecoration => this.decorations != null && this.decorations.Count > 0;

    public IDecoration Decoration
    {
      get => !this.HasDecoration ? (IDecoration) null : this.Decorations[0];
      set
      {
        this.Decorations.Clear();
        if (value == null)
          return;
        this.Decorations.Add(value);
      }
    }

    public IList<IDecoration> Decorations
    {
      get
      {
        if (this.decorations == null)
          this.decorations = (IList<IDecoration>) new List<IDecoration>();
        return this.decorations;
      }
    }

    public object ImageSelector
    {
      get => this.imageSelector;
      set => this.imageSelector = value;
    }

    public string Url
    {
      get => this.url;
      set => this.url = value;
    }
  }
}
