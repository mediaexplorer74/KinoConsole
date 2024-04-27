// Decompiled with JetBrains decompiler
// Type: TAFactory.IconPack.IconListView
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace TAFactory.IconPack
{
  public class IconListView : ListView
  {
    private const int minWidth = 64;
    private const int textHeight = 18;
    private const int verticalSpacing = 5;
    private static readonly Padding TilePadding = new Padding(5, 1, 5, 1);
    private Size _tileSize;
    private IContainer components;

    public IconListView()
    {
      this.InitializeComponent();
      this.View = View.Tile;
      this.TileSize = base.TileSize;
      this.OwnerDraw = true;
      this.DrawItem += new DrawListViewItemEventHandler(this.IconListView_DrawItem);
    }

    public new Size TileSize
    {
      get => this._tileSize;
      set
      {
        this._tileSize = value;
        this.BeginUpdate();
        base.TileSize = new Size(Math.Max(64, value.Width) + IconListView.TilePadding.Horizontal, value.Height + 5 + 18 + IconListView.TilePadding.Vertical);
        if (this.Items.Count != 0)
        {
          List<IconListViewItem> iconListViewItemList = new List<IconListViewItem>(this.Items.Count);
          foreach (IconListViewItem iconListViewItem in this.Items)
            iconListViewItemList.Add(iconListViewItem);
          this.Items.Clear();
          foreach (ListViewItem listViewItem in iconListViewItemList)
            this.Items.Add(listViewItem);
        }
        this.EndUpdate();
      }
    }

    private void IconListView_DrawItem(object sender, DrawListViewItemEventArgs e)
    {
      if (!(e.Item is IconListViewItem iconListViewItem))
      {
        e.DrawDefault = true;
      }
      else
      {
        e.DrawBackground();
        Pen pen = SystemPens.ControlLight;
        if (e.Item.Selected)
          pen = !this.Focused ? SystemPens.ButtonFace : SystemPens.Highlight;
        int num1 = (e.Bounds.Width - this.TileSize.Width - IconListView.TilePadding.Horizontal) / 2 + IconListView.TilePadding.Left;
        Rectangle rect = new Rectangle(e.Bounds.X + num1, e.Bounds.Y + IconListView.TilePadding.Top, this.TileSize.Width, this.TileSize.Height);
        e.Graphics.DrawRectangle(pen, rect);
        Rectangle targetRect = new Rectangle(e.Bounds.X + (rect.Width - iconListViewItem.Icon.Width) / 2 + num1 + 1, e.Bounds.Y + (rect.Height - iconListViewItem.Icon.Height) / 2 + IconListView.TilePadding.Top + 1, iconListViewItem.Icon.Width, iconListViewItem.Icon.Height);
        Region region1 = new Region(rect);
        e.Graphics.Clip = region1;
        e.Graphics.DrawIcon(iconListViewItem.Icon, targetRect);
        string str = string.Format("{0} x {1}", (object) iconListViewItem.Icon.Width, (object) iconListViewItem.Icon.Height);
        SizeF sizeF = e.Graphics.MeasureString(str, this.Font);
        int num2 = (int) Math.Round((double) sizeF.Width);
        Math.Round((double) sizeF.Height);
        int x = e.Bounds.X + (e.Bounds.Width - num2 - IconListView.TilePadding.Horizontal) / 2 + IconListView.TilePadding.Left;
        int y = e.Bounds.Y + this.TileSize.Height + 5 + IconListView.TilePadding.Top;
        Region region2 = new Region(e.Bounds);
        e.Graphics.Clip = region2;
        if (e.Item.Selected)
        {
          if (this.Focused)
          {
            e.Graphics.FillRectangle(SystemBrushes.Highlight, (float) (x - 1), (float) (y - 1), (float) (num2 + 2), sizeF.Height + 2f);
            e.Graphics.DrawString(str, this.Font, SystemBrushes.HighlightText, (float) x, (float) y);
          }
          else
          {
            e.Graphics.FillRectangle(SystemBrushes.ButtonFace, (float) (x - 1), (float) (y - 1), (float) (num2 + 2), sizeF.Height + 2f);
            e.Graphics.DrawString(str, this.Font, SystemBrushes.ControlText, (float) x, (float) y);
          }
        }
        else
          e.Graphics.DrawString(str, this.Font, SystemBrushes.ControlText, (float) x, (float) y);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
  }
}
