// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.BaseRenderer
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace BrightIdeasSoftware
{
  [ToolboxItem(true)]
  [Browsable(true)]
  public class BaseRenderer : AbstractRenderer
  {
    private bool canWrap;
    private Rectangle? cellPadding;
    private StringAlignment? cellVerticalAlignment;
    private ImageList imageList;
    private int spacing = 1;
    private bool useGdiTextRendering = true;
    private object aspect;
    private Rectangle bounds;
    private OLVColumn column;
    private DrawListViewItemEventArgs drawItemEventArgs;
    private DrawListViewSubItemEventArgs eventArgs;
    private Font font;
    private bool isItemSelected;
    private bool isPrinting;
    private OLVListItem listItem;
    private ObjectListView objectListView;
    private object rowObject;
    private OLVListSubItem listSubItem;
    private Brush textBrush;

    [Description("Can the renderer wrap text that does not fit completely within the cell")]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool CanWrap
    {
      get => this.canWrap;
      set
      {
        this.canWrap = value;
        if (!this.canWrap)
          return;
        this.UseGdiTextRendering = false;
      }
    }

    [Description("The number of pixels that renderer will leave empty around the edge of the cell")]
    [DefaultValue(null)]
    [Category("ObjectListView")]
    public Rectangle? CellPadding
    {
      get => this.cellPadding;
      set => this.cellPadding = value;
    }

    [DefaultValue(null)]
    [Description("How will cell values be vertically aligned?")]
    [Category("ObjectListView")]
    public virtual StringAlignment? CellVerticalAlignment
    {
      get => this.cellVerticalAlignment;
      set => this.cellVerticalAlignment = value;
    }

    [Browsable(false)]
    protected virtual Rectangle? EffectiveCellPadding
    {
      get
      {
        if (this.cellPadding.HasValue)
          return new Rectangle?(this.cellPadding.Value);
        if (this.OLVSubItem != null && this.OLVSubItem.CellPadding.HasValue)
          return new Rectangle?(this.OLVSubItem.CellPadding.Value);
        if (this.ListItem != null && this.ListItem.CellPadding.HasValue)
          return new Rectangle?(this.ListItem.CellPadding.Value);
        if (this.Column != null && this.Column.CellPadding.HasValue)
          return new Rectangle?(this.Column.CellPadding.Value);
        return this.ListView != null && this.ListView.CellPadding.HasValue ? new Rectangle?(this.ListView.CellPadding.Value) : new Rectangle?();
      }
    }

    [Browsable(false)]
    protected virtual StringAlignment EffectiveCellVerticalAlignment
    {
      get
      {
        if (this.cellVerticalAlignment.HasValue)
          return this.cellVerticalAlignment.Value;
        if (this.OLVSubItem != null && this.OLVSubItem.CellVerticalAlignment.HasValue)
          return this.OLVSubItem.CellVerticalAlignment.Value;
        if (this.ListItem != null && this.ListItem.CellVerticalAlignment.HasValue)
          return this.ListItem.CellVerticalAlignment.Value;
        if (this.Column != null && this.Column.CellVerticalAlignment.HasValue)
          return this.Column.CellVerticalAlignment.Value;
        return this.ListView != null ? this.ListView.CellVerticalAlignment : StringAlignment.Center;
      }
    }

    [Category("Appearance")]
    [Description("The image list from which keyed images will be fetched for drawing.")]
    [DefaultValue(null)]
    public ImageList ImageList
    {
      get => this.imageList;
      set => this.imageList = value;
    }

    [Description("When rendering multiple images, how many pixels should be between each image?")]
    [Category("Appearance")]
    [DefaultValue(1)]
    public int Spacing
    {
      get => this.spacing;
      set => this.spacing = value;
    }

    [Category("Appearance")]
    [Description("Should text be rendered using GDI routines?")]
    [DefaultValue(true)]
    public bool UseGdiTextRendering
    {
      get => !this.IsPrinting && this.useGdiTextRendering;
      set => this.useGdiTextRendering = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object Aspect
    {
      get
      {
        if (this.aspect == null)
          this.aspect = this.column.GetValue(this.rowObject);
        return this.aspect;
      }
      set => this.aspect = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle Bounds
    {
      get => this.bounds;
      set => this.bounds = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public OLVColumn Column
    {
      get => this.column;
      set => this.column = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DrawListViewItemEventArgs DrawItemEvent
    {
      get => this.drawItemEventArgs;
      set => this.drawItemEventArgs = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DrawListViewSubItemEventArgs Event
    {
      get => this.eventArgs;
      set => this.eventArgs = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Font Font
    {
      get
      {
        if (this.font != null || this.ListItem == null)
          return this.font;
        return this.SubItem == null || this.ListItem.UseItemStyleForSubItems ? this.ListItem.Font : this.SubItem.Font;
      }
      set => this.font = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ImageList ImageListOrDefault => this.ImageList ?? this.ListView.SmallImageList;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsDrawBackground => !this.IsPrinting;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsItemSelected
    {
      get => this.isItemSelected;
      set => this.isItemSelected = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsPrinting
    {
      get => this.isPrinting;
      set => this.isPrinting = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public OLVListItem ListItem
    {
      get => this.listItem;
      set => this.listItem = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ObjectListView ListView
    {
      get => this.objectListView;
      set => this.objectListView = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public OLVListSubItem OLVSubItem => this.listSubItem;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object RowObject
    {
      get => this.rowObject;
      set => this.rowObject = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public OLVListSubItem SubItem
    {
      get => this.listSubItem;
      set => this.listSubItem = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Brush TextBrush
    {
      get
      {
        return this.textBrush == null ? (Brush) new SolidBrush(this.GetForegroundColor()) : this.textBrush;
      }
      set => this.textBrush = value;
    }

    private void ClearState()
    {
      this.Event = (DrawListViewSubItemEventArgs) null;
      this.DrawItemEvent = (DrawListViewItemEventArgs) null;
      this.Aspect = (object) null;
      this.Font = (Font) null;
      this.TextBrush = (Brush) null;
    }

    protected virtual Rectangle AlignRectangle(Rectangle outer, Rectangle inner)
    {
      Rectangle rectangle = new Rectangle(outer.Location, inner.Size);
      if (inner.Width < outer.Width)
        rectangle.X = this.AlignHorizontally(outer, inner);
      if (inner.Height < outer.Height)
        rectangle.Y = this.AlignVertically(outer, inner);
      return rectangle;
    }

    protected int AlignHorizontally(Rectangle outer, Rectangle inner)
    {
      switch (this.Column == null ? 0 : (int) this.Column.TextAlign)
      {
        case 0:
          return outer.Left + 1;
        case 1:
          return outer.Right - inner.Width - 1;
        case 2:
          return outer.Left + (outer.Width - inner.Width) / 2;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    protected int AlignVertically(Rectangle outer, Rectangle inner)
    {
      return this.AlignVertically(outer, inner.Height);
    }

    protected int AlignVertically(Rectangle outer, int innerHeight)
    {
      switch (this.EffectiveCellVerticalAlignment)
      {
        case StringAlignment.Near:
          return outer.Top + 1;
        case StringAlignment.Center:
          return outer.Top + (outer.Height - innerHeight) / 2;
        case StringAlignment.Far:
          return outer.Bottom - innerHeight - 1;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    protected virtual Rectangle CalculateAlignedRectangle(Graphics g, Rectangle r)
    {
      if (this.Column == null || this.Column.TextAlign == HorizontalAlignment.Left)
        return r;
      int width = this.CalculateCheckBoxWidth(g) + this.CalculateImageWidth(g, this.GetImageSelector()) + this.CalculateTextWidth(g, this.GetText());
      return width >= r.Width ? r : this.AlignRectangle(r, new Rectangle(0, 0, width, r.Height));
    }

    protected virtual int CalculateCheckBoxWidth(Graphics g)
    {
      return this.ListView.CheckBoxes && this.ColumnIsPrimary ? CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal).Width + 6 : 0;
    }

    protected virtual int CalculateImageWidth(Graphics g, object imageSelector)
    {
      if (imageSelector == null || imageSelector == DBNull.Value)
        return 0;
      ImageList imageListOrDefault = this.ImageListOrDefault;
      if (imageListOrDefault != null)
      {
        int num = -1;
        if (!(imageSelector is int) && imageSelector is string key)
          num = imageListOrDefault.Images.IndexOfKey(key);
        if (num >= 0)
          return imageListOrDefault.ImageSize.Width;
      }
      return imageSelector is Image image ? image.Width : 0;
    }

    protected virtual int CalculateTextWidth(Graphics g, string txt)
    {
      if (string.IsNullOrEmpty(txt))
        return 0;
      if (this.UseGdiTextRendering)
      {
        Size proposedSize = new Size(int.MaxValue, int.MaxValue);
        return TextRenderer.MeasureText((IDeviceContext) g, txt, this.Font, proposedSize, TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix).Width;
      }
      using (StringFormat format = new StringFormat())
      {
        format.Trimming = StringTrimming.EllipsisCharacter;
        return 1 + (int) g.MeasureString(txt, this.Font, int.MaxValue, format).Width;
      }
    }

    public virtual Color GetBackgroundColor()
    {
      if (!this.ListView.Enabled)
        return SystemColors.Control;
      if (this.IsItemSelected && !this.ListView.UseTranslucentSelection && this.ListView.FullRowSelect)
      {
        if (this.ListView.Focused)
          return this.ListView.HighlightBackgroundColorOrDefault;
        if (!this.ListView.HideSelection)
          return this.ListView.UnfocusedHighlightBackgroundColorOrDefault;
      }
      return this.SubItem == null || this.ListItem.UseItemStyleForSubItems ? this.ListItem.BackColor : this.SubItem.BackColor;
    }

    public virtual Color GetForegroundColor()
    {
      if (this.IsItemSelected && !this.ListView.UseTranslucentSelection && (this.ColumnIsPrimary || this.ListView.FullRowSelect))
      {
        if (this.ListView.Focused)
          return this.ListView.HighlightForegroundColorOrDefault;
        if (!this.ListView.HideSelection)
          return this.ListView.UnfocusedHighlightForegroundColorOrDefault;
      }
      return this.SubItem == null || this.ListItem.UseItemStyleForSubItems ? this.ListItem.ForeColor : this.SubItem.ForeColor;
    }

    protected virtual Image GetImage() => this.GetImage(this.GetImageSelector());

    protected virtual Image GetImage(object imageSelector)
    {
      if (imageSelector == null || imageSelector == DBNull.Value)
        return (Image) null;
      ImageList imageListOrDefault = this.ImageListOrDefault;
      if (imageListOrDefault != null)
      {
        switch (imageSelector)
        {
          case int index:
            return index < 0 || index >= imageListOrDefault.Images.Count ? (Image) null : imageListOrDefault.Images[index];
          case string key:
            return imageListOrDefault.Images.ContainsKey(key) ? imageListOrDefault.Images[key] : (Image) null;
        }
      }
      return imageSelector as Image;
    }

    protected virtual object GetImageSelector()
    {
      return !this.ColumnIsPrimary ? this.OLVSubItem.ImageSelector : this.ListItem.ImageSelector;
    }

    protected virtual string GetText()
    {
      return this.SubItem != null ? this.SubItem.Text : this.ListItem.Text;
    }

    protected virtual Color GetTextBackgroundColor()
    {
      if (this.IsItemSelected && !this.ListView.UseTranslucentSelection && (this.ColumnIsPrimary || this.ListView.FullRowSelect))
      {
        if (this.ListView.Focused)
          return this.ListView.HighlightBackgroundColorOrDefault;
        if (!this.ListView.HideSelection)
          return this.ListView.UnfocusedHighlightBackgroundColorOrDefault;
      }
      return this.SubItem == null || this.ListItem.UseItemStyleForSubItems ? this.ListItem.BackColor : this.SubItem.BackColor;
    }

    public override bool RenderItem(
      DrawListViewItemEventArgs e,
      Graphics g,
      Rectangle itemBounds,
      object rowObject)
    {
      this.ClearState();
      this.DrawItemEvent = e;
      this.ListItem = (OLVListItem) e.Item;
      this.SubItem = (OLVListSubItem) null;
      this.ListView = (ObjectListView) this.ListItem.ListView;
      this.Column = this.ListView.GetColumn(0);
      this.RowObject = rowObject;
      this.Bounds = itemBounds;
      this.IsItemSelected = this.ListItem.Selected;
      return this.OptionalRender(g, itemBounds);
    }

    public override bool RenderSubItem(
      DrawListViewSubItemEventArgs e,
      Graphics g,
      Rectangle cellBounds,
      object rowObject)
    {
      this.ClearState();
      this.Event = e;
      this.ListItem = (OLVListItem) e.Item;
      this.SubItem = (OLVListSubItem) e.SubItem;
      this.ListView = (ObjectListView) this.ListItem.ListView;
      this.Column = (OLVColumn) e.Header;
      this.RowObject = rowObject;
      this.Bounds = cellBounds;
      this.IsItemSelected = this.ListItem.Selected;
      return this.OptionalRender(g, cellBounds);
    }

    public override void HitTest(OlvListViewHitTestInfo hti, int x, int y)
    {
      this.ClearState();
      this.ListView = hti.ListView;
      this.ListItem = hti.Item;
      this.SubItem = hti.SubItem;
      this.Column = hti.Column;
      this.RowObject = hti.RowObject;
      this.IsItemSelected = this.ListItem.Selected;
      this.Bounds = this.SubItem != null ? this.ListItem.GetSubItemBounds(this.Column.Index) : this.ListItem.Bounds;
      using (Graphics graphics = this.ListView.CreateGraphics())
        this.HandleHitTest(graphics, hti, x, y);
    }

    public override Rectangle GetEditRectangle(
      Graphics g,
      Rectangle cellBounds,
      OLVListItem item,
      int subItemIndex,
      Size preferredSize)
    {
      this.ClearState();
      this.ListView = (ObjectListView) item.ListView;
      this.ListItem = item;
      this.SubItem = item.GetSubItem(subItemIndex);
      this.Column = this.ListView.GetColumn(subItemIndex);
      this.RowObject = item.RowObject;
      this.IsItemSelected = this.ListItem.Selected;
      this.Bounds = cellBounds;
      return this.HandleGetEditRectangle(g, cellBounds, item, subItemIndex, preferredSize);
    }

    public virtual bool OptionalRender(Graphics g, Rectangle r)
    {
      if (this.ListView.View != View.Details)
        return false;
      this.Render(g, r);
      return true;
    }

    public virtual void Render(Graphics g, Rectangle r) => this.StandardRender(g, r);

    protected virtual void HandleHitTest(Graphics g, OlvListViewHitTestInfo hti, int x, int y)
    {
      Rectangle alignedRectangle = this.CalculateAlignedRectangle(g, this.Bounds);
      this.StandardHitTest(g, hti, alignedRectangle, x, y);
    }

    protected virtual Rectangle HandleGetEditRectangle(
      Graphics g,
      Rectangle cellBounds,
      OLVListItem item,
      int subItemIndex,
      Size preferredSize)
    {
      return (object) this.GetType() == (object) typeof (BaseRenderer) ? this.StandardGetEditRectangle(g, cellBounds, preferredSize) : cellBounds;
    }

    protected void StandardRender(Graphics g, Rectangle r)
    {
      this.DrawBackground(g, r);
      if (this.ColumnIsPrimary)
      {
        r.X += 3;
        --r.Width;
      }
      r = this.ApplyCellPadding(r);
      this.DrawAlignedImageAndText(g, r);
      if (!ObjectListView.ShowCellPaddingBounds)
        return;
      g.DrawRectangle(Pens.Purple, r);
    }

    public virtual Rectangle ApplyCellPadding(Rectangle r)
    {
      Rectangle? effectiveCellPadding = this.EffectiveCellPadding;
      if (!effectiveCellPadding.HasValue)
        return r;
      Rectangle rectangle = effectiveCellPadding.Value;
      r.Width -= rectangle.Right;
      r.Height -= rectangle.Bottom;
      r.Offset(rectangle.Location);
      return r;
    }

    protected void StandardHitTest(
      Graphics g,
      OlvListViewHitTestInfo hti,
      Rectangle bounds,
      int x,
      int y)
    {
      Rectangle r = bounds;
      if (this.ColumnIsPrimary && !(this is TreeListView.TreeRenderer))
      {
        r.X += 3;
        --r.Width;
      }
      Rectangle cellBounds = this.ApplyCellPadding(r);
      int num = 0;
      if (this.ColumnIsPrimary && this.ListView.CheckBoxes)
      {
        Rectangle checkBoxBounds = this.CalculateCheckBoxBounds(g, cellBounds);
        checkBoxBounds.Inflate(2, 2);
        if (checkBoxBounds.Contains(x, y))
        {
          hti.HitTestLocation = HitTestLocation.CheckBox;
          return;
        }
        num = checkBoxBounds.Width;
      }
      cellBounds.X += num;
      cellBounds.Width -= num;
      int imageWidth = this.CalculateImageWidth(g, this.GetImageSelector());
      Rectangle rectangle = cellBounds with
      {
        Width = imageWidth
      };
      if (rectangle.Contains(x, y))
      {
        if (this.Column != null && this.Column.Index > 0 && this.Column.CheckBoxes)
          hti.HitTestLocation = HitTestLocation.CheckBox;
        else
          hti.HitTestLocation = HitTestLocation.Image;
      }
      else
      {
        cellBounds.X += imageWidth;
        cellBounds.Width -= imageWidth;
        int textWidth = this.CalculateTextWidth(g, this.GetText());
        rectangle = cellBounds with { Width = textWidth };
        if (rectangle.Contains(x, y))
          hti.HitTestLocation = HitTestLocation.Text;
        else
          hti.HitTestLocation = HitTestLocation.InCell;
      }
    }

    protected Rectangle StandardGetEditRectangle(
      Graphics g,
      Rectangle cellBounds,
      Size preferredSize)
    {
      Rectangle alignedRectangle = this.CalculateAlignedRectangle(g, cellBounds);
      Rectangle paddedAlignedBounds = this.CalculatePaddedAlignedBounds(g, alignedRectangle, preferredSize);
      int num = this.CalculateCheckBoxWidth(g) + this.CalculateImageWidth(g, this.GetImageSelector());
      if (this.ColumnIsPrimary && this.ListItem.IndentCount > 0)
      {
        int width = this.ListView.SmallImageSize.Width;
        num += width * this.ListItem.IndentCount;
      }
      if (num > 0)
      {
        paddedAlignedBounds.X += num;
        paddedAlignedBounds.Width = Math.Max(paddedAlignedBounds.Width - num, 40);
      }
      return paddedAlignedBounds;
    }

    protected Rectangle CalculatePaddedAlignedBounds(
      Graphics g,
      Rectangle bounds,
      Size preferredSize)
    {
      Rectangle outer = this.ApplyCellPadding(bounds);
      return this.AlignRectangle(outer, new Rectangle(0, 0, outer.Width, preferredSize.Height));
    }

    protected virtual void DrawAlignedImage(Graphics g, Rectangle r, Image image)
    {
      if (image == null)
        return;
      Rectangle inner = new Rectangle(r.Location, image.Size);
      if (image.Height > r.Height)
      {
        float num = (float) r.Height / (float) image.Height;
        inner.Width = (int) ((double) image.Width * (double) num);
        inner.Height = r.Height - 1;
      }
      g.DrawImage(image, this.AlignRectangle(r, inner));
    }

    protected virtual void DrawAlignedImageAndText(Graphics g, Rectangle r)
    {
      this.DrawImageAndText(g, this.CalculateAlignedRectangle(g, r));
    }

    protected virtual void DrawBackground(Graphics g, Rectangle r)
    {
      if (!this.IsDrawBackground)
        return;
      using (Brush brush = (Brush) new SolidBrush(this.GetBackgroundColor()))
        g.FillRectangle(brush, r.X - 1, r.Y - 1, r.Width + 2, r.Height + 2);
    }

    protected virtual int DrawCheckBox(Graphics g, Rectangle r)
    {
      int stateImageIndex = this.ListItem.StateImageIndex;
      if (this.IsPrinting)
        return this.ListView.StateImageList == null || stateImageIndex < 0 ? 0 : this.DrawImage(g, r, (object) this.ListView.StateImageList.Images[stateImageIndex]) + 4;
      r = this.CalculateCheckBoxBounds(g, r);
      CheckBoxState checkBoxState = this.GetCheckBoxState(this.ListItem.CheckState);
      CheckBoxRenderer.DrawCheckBox(g, r.Location, checkBoxState);
      return CheckBoxRenderer.GetGlyphSize(g, checkBoxState).Width + 6;
    }

    protected virtual CheckBoxState GetCheckBoxState(CheckState checkState)
    {
      if (this.IsCheckBoxDisabled)
      {
        switch (checkState)
        {
          case CheckState.Unchecked:
            return CheckBoxState.UncheckedDisabled;
          case CheckState.Checked:
            return CheckBoxState.CheckedDisabled;
          default:
            return CheckBoxState.MixedDisabled;
        }
      }
      else if (this.IsItemHot)
      {
        switch (checkState)
        {
          case CheckState.Unchecked:
            return CheckBoxState.UncheckedHot;
          case CheckState.Checked:
            return CheckBoxState.CheckedHot;
          default:
            return CheckBoxState.MixedHot;
        }
      }
      else
      {
        switch (checkState)
        {
          case CheckState.Unchecked:
            return CheckBoxState.UncheckedNormal;
          case CheckState.Checked:
            return CheckBoxState.CheckedNormal;
          default:
            return CheckBoxState.MixedNormal;
        }
      }
    }

    protected virtual bool IsCheckBoxDisabled
    {
      get
      {
        if (!this.ListView.RenderNonEditableCheckboxesAsDisabled)
          return false;
        if (this.ListView.CellEditActivation == ObjectListView.CellEditActivateMode.None)
          return true;
        return this.Column != null && !this.Column.IsEditable;
      }
    }

    protected bool IsItemHot
    {
      get
      {
        return this.ListView != null && this.ListItem != null && this.ListView.HotRowIndex == this.ListItem.Index && this.ListView.HotColumnIndex == (this.Column == null ? 0 : this.Column.Index) && this.ListView.HotCellHitLocation == HitTestLocation.CheckBox;
      }
    }

    protected Rectangle CalculateCheckBoxBounds(Graphics g, Rectangle cellBounds)
    {
      Size glyphSize = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.CheckedNormal);
      return this.AlignRectangle(cellBounds, new Rectangle(0, 0, glyphSize.Width, glyphSize.Height));
    }

    protected virtual int DrawImage(Graphics g, Rectangle r, object imageSelector)
    {
      if (imageSelector == null || imageSelector == DBNull.Value)
        return 0;
      ImageList smallImageList = this.ListView.SmallImageList;
      if (smallImageList != null)
      {
        int index = -1;
        if (imageSelector is int)
        {
          if (index >= smallImageList.Images.Count)
            index = -1;
        }
        else if (imageSelector is string key)
          index = smallImageList.Images.IndexOfKey(key);
        if (index >= 0)
        {
          if (this.IsPrinting)
          {
            imageSelector = (object) smallImageList.Images[index];
          }
          else
          {
            if (smallImageList.ImageSize.Height < r.Height)
              r.Y = this.AlignVertically(r, new Rectangle(Point.Empty, smallImageList.ImageSize));
            Rectangle rectangle = new Rectangle(r.X - this.Bounds.X, r.Y - this.Bounds.Y, r.Width, r.Height);
            smallImageList.Draw(g, rectangle.Location, index);
            return smallImageList.ImageSize.Width;
          }
        }
      }
      if (!(imageSelector is Image image))
        return 0;
      int y = r.Y;
      if (image.Size.Height < r.Height)
        r.Y = this.AlignVertically(r, new Rectangle(Point.Empty, image.Size));
      g.DrawImageUnscaled(image, r.X, y);
      return image.Width;
    }

    protected virtual void DrawImageAndText(Graphics g, Rectangle r)
    {
      if (this.ListView.CheckBoxes && this.ColumnIsPrimary)
      {
        int num = this.DrawCheckBox(g, r);
        r.X += num;
        r.Width -= num;
      }
      int num1 = this.DrawImage(g, r, this.GetImageSelector());
      r.X += num1;
      r.Width -= num1;
      this.DrawText(g, r, this.GetText());
    }

    protected virtual int DrawImages(Graphics g, Rectangle r, ICollection imageSelectors)
    {
      List<Image> imageList = new List<Image>();
      foreach (object imageSelector in (IEnumerable) imageSelectors)
      {
        Image image = this.GetImage(imageSelector);
        if (image != null)
          imageList.Add(image);
      }
      int width = 0;
      int num = 0;
      foreach (Image image in imageList)
      {
        width += image.Width + this.Spacing;
        num = Math.Max(num, image.Height);
      }
      Point location = this.AlignRectangle(r, new Rectangle(0, 0, width, num)).Location;
      foreach (Image image in imageList)
      {
        g.DrawImage(image, location);
        location.X += image.Width + this.Spacing;
      }
      return width;
    }

    public virtual void DrawText(Graphics g, Rectangle r, string txt)
    {
      if (string.IsNullOrEmpty(txt))
        return;
      if (this.UseGdiTextRendering)
        this.DrawTextGdi(g, r, txt);
      else
        this.DrawTextGdiPlus(g, r, txt);
    }

    protected virtual void DrawTextGdi(Graphics g, Rectangle r, string txt)
    {
      Color backColor = Color.Transparent;
      if (this.IsDrawBackground && this.IsItemSelected && this.ColumnIsPrimary && !this.ListView.FullRowSelect)
        backColor = this.GetTextBackgroundColor();
      TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.PreserveGraphicsTranslateTransform | this.CellVerticalAlignmentAsTextFormatFlag;
      if (!this.CanWrap)
        flags |= TextFormatFlags.SingleLine;
      TextRenderer.DrawText((IDeviceContext) g, txt, this.Font, r, this.GetForegroundColor(), backColor, flags);
    }

    private bool ColumnIsPrimary => this.Column != null && this.Column.Index == 0;

    protected TextFormatFlags CellVerticalAlignmentAsTextFormatFlag
    {
      get
      {
        switch (this.EffectiveCellVerticalAlignment)
        {
          case StringAlignment.Near:
            return TextFormatFlags.Default;
          case StringAlignment.Center:
            return TextFormatFlags.VerticalCenter;
          case StringAlignment.Far:
            return TextFormatFlags.Bottom;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    protected virtual StringFormat StringFormatForGdiPlus
    {
      get
      {
        StringFormat formatForGdiPlus = new StringFormat();
        formatForGdiPlus.LineAlignment = this.EffectiveCellVerticalAlignment;
        formatForGdiPlus.Trimming = StringTrimming.EllipsisCharacter;
        formatForGdiPlus.Alignment = this.Column == null ? StringAlignment.Near : this.Column.TextStringAlign;
        if (!this.CanWrap)
          formatForGdiPlus.FormatFlags = StringFormatFlags.NoWrap;
        return formatForGdiPlus;
      }
    }

    protected virtual void DrawTextGdiPlus(Graphics g, Rectangle r, string txt)
    {
      using (StringFormat formatForGdiPlus = this.StringFormatForGdiPlus)
      {
        Font font = this.Font;
        if (this.IsDrawBackground && this.IsItemSelected && this.ColumnIsPrimary && !this.ListView.FullRowSelect)
        {
          SizeF sizeF = g.MeasureString(txt, font, r.Width, formatForGdiPlus);
          Rectangle rect = r with
          {
            Width = (int) sizeF.Width + 1
          };
          using (Brush brush = (Brush) new SolidBrush(this.ListView.HighlightBackgroundColorOrDefault))
            g.FillRectangle(brush, rect);
        }
        RectangleF layoutRectangle = (RectangleF) r;
        g.DrawString(txt, font, this.TextBrush, layoutRectangle, formatForGdiPlus);
      }
    }
  }
}
