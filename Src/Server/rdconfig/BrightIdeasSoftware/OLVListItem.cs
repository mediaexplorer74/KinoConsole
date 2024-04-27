﻿// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.OLVListItem
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class OLVListItem : ListViewItem
  {
    private Rectangle? cellPadding;
    private StringAlignment? cellVerticalAlignment;
    private CheckState checkState;
    private IList<IDecoration> decorations;
    private object imageSelector;
    private object rowObject;

    public OLVListItem(object rowObject) => this.rowObject = rowObject;

    public OLVListItem(object rowObject, string text, object image)
      : base(text, -1)
    {
      this.rowObject = rowObject;
      this.imageSelector = image;
    }

    public new Rectangle Bounds
    {
      get
      {
        try
        {
          return base.Bounds;
        }
        catch (ArgumentException ex)
        {
          return Rectangle.Empty;
        }
      }
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

    public new bool Checked
    {
      get => base.Checked;
      set
      {
        if (this.Checked == value)
          return;
        if (value)
          ((ObjectListView) this.ListView).CheckObject(this.RowObject);
        else
          ((ObjectListView) this.ListView).UncheckObject(this.RowObject);
      }
    }

    public CheckState CheckState
    {
      get
      {
        switch (this.StateImageIndex)
        {
          case 0:
            return CheckState.Unchecked;
          case 1:
            return CheckState.Checked;
          case 2:
            return CheckState.Indeterminate;
          default:
            return CheckState.Unchecked;
        }
      }
      set
      {
        if (this.checkState == value)
          return;
        this.checkState = value;
        switch (value)
        {
          case CheckState.Unchecked:
            this.StateImageIndex = 0;
            break;
          case CheckState.Checked:
            this.StateImageIndex = 1;
            break;
          case CheckState.Indeterminate:
            this.StateImageIndex = 2;
            break;
        }
      }
    }

    public bool HasDecoration => this.decorations != null && this.decorations.Count > 0;

    public IDecoration Decoration
    {
      get => this.HasDecoration ? this.Decorations[0] : (IDecoration) null;
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
      set
      {
        this.imageSelector = value;
        switch (value)
        {
          case int num:
            this.ImageIndex = num;
            break;
          case string _:
            this.ImageKey = (string) value;
            break;
          default:
            this.ImageIndex = -1;
            break;
        }
      }
    }

    public object RowObject
    {
      get => this.rowObject;
      set => this.rowObject = value;
    }

    public virtual OLVListSubItem GetSubItem(int index)
    {
      return index >= 0 && index < this.SubItems.Count ? (OLVListSubItem) this.SubItems[index] : (OLVListSubItem) null;
    }

    public virtual Rectangle GetSubItemBounds(int subItemIndex)
    {
      if (subItemIndex == 0)
      {
        Rectangle bounds = this.Bounds;
        Point scrolledColumnSides = NativeMethods.GetScrolledColumnSides(this.ListView, subItemIndex);
        bounds.X = scrolledColumnSides.X + 1;
        bounds.Width = scrolledColumnSides.Y - scrolledColumnSides.X;
        return bounds;
      }
      OLVListSubItem subItem = this.GetSubItem(subItemIndex);
      return subItem != null ? subItem.Bounds : new Rectangle();
    }
  }
}
