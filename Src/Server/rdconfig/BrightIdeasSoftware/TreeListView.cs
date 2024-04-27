// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.TreeListView
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace BrightIdeasSoftware
{
  public class TreeListView : VirtualObjectListView
  {
    private bool revealAfterExpand = true;
    private TreeListView.TreeRenderer treeRenderer;
    private bool useWaitCursorWhenExpanding = true;
    private TreeListView.Tree treeModel;

    [Description("This event is triggered when a branch is about to expand.")]
    [Category("ObjectListView")]
    public event EventHandler<TreeBranchExpandingEventArgs> Expanding;

    [Category("ObjectListView")]
    [Description("This event is triggered when a branch is about to collapsed.")]
    public event EventHandler<TreeBranchCollapsingEventArgs> Collapsing;

    [Category("ObjectListView")]
    [Description("This event is triggered when a branch has been expanded.")]
    public event EventHandler<TreeBranchExpandedEventArgs> Expanded;

    [Description("This event is triggered when a branch has been collapsed.")]
    [Category("ObjectListView")]
    public event EventHandler<TreeBranchCollapsedEventArgs> Collapsed;

    protected virtual void OnExpanding(TreeBranchExpandingEventArgs e)
    {
      if (this.Expanding == null)
        return;
      this.Expanding((object) this, e);
    }

    protected virtual void OnCollapsing(TreeBranchCollapsingEventArgs e)
    {
      if (this.Collapsing == null)
        return;
      this.Collapsing((object) this, e);
    }

    protected virtual void OnExpanded(TreeBranchExpandedEventArgs e)
    {
      if (this.Expanded == null)
        return;
      this.Expanded((object) this, e);
    }

    protected virtual void OnCollapsed(TreeBranchCollapsedEventArgs e)
    {
      if (this.Collapsed == null)
        return;
      this.Collapsed((object) this, e);
    }

    public TreeListView()
    {
      this.TreeModel = new TreeListView.Tree(this);
      this.OwnerDraw = true;
      this.View = View.Details;
      this.VirtualListDataSource = (IVirtualListDataSource) this.TreeModel;
      this.TreeColumnRenderer = new TreeListView.TreeRenderer();
      this.StateImageList = new ImageList();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual TreeListView.CanExpandGetterDelegate CanExpandGetter
    {
      get => this.TreeModel.CanExpandGetter;
      set => this.TreeModel.CanExpandGetter = value;
    }

    [Browsable(false)]
    public override bool CanShowGroups => false;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual TreeListView.ChildrenGetterDelegate ChildrenGetter
    {
      get => this.TreeModel.ChildrenGetter;
      set => this.TreeModel.ChildrenGetter = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IEnumerable ExpandedObjects
    {
      get => (IEnumerable) this.TreeModel.mapObjectToExpanded.Keys;
      set
      {
        this.TreeModel.mapObjectToExpanded.Clear();
        if (value == null)
          return;
        foreach (object model in value)
          this.TreeModel.SetModelExpanded(model, true);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override IListFilter ListFilter
    {
      get => (IListFilter) null;
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override IEnumerable Objects
    {
      get => this.Roots;
      set => this.Roots = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override IEnumerable ObjectsForClustering
    {
      get
      {
        for (int i = 0; i < this.TreeModel.GetObjectCount(); ++i)
          yield return this.TreeModel.GetNthObject(i);
      }
    }

    [Category("ObjectListView")]
    [Description("Should the parent of an expand subtree be scrolled to the top revealing the children?")]
    [DefaultValue(true)]
    public bool RevealAfterExpand
    {
      get => this.revealAfterExpand;
      set => this.revealAfterExpand = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IEnumerable Roots
    {
      get => this.TreeModel.RootObjects;
      set
      {
        this.TreeColumnRenderer = this.TreeColumnRenderer;
        this.TreeModel.RootObjects = value ?? (IEnumerable) new ArrayList();
        this.UpdateVirtualListSize();
      }
    }

    protected virtual void EnsureTreeRendererPresent(TreeListView.TreeRenderer renderer)
    {
      if (this.Columns.Count == 0)
        return;
      foreach (OLVColumn column in this.Columns)
      {
        if (column.Renderer is TreeListView.TreeRenderer)
        {
          column.Renderer = (IRenderer) renderer;
          return;
        }
      }
      OLVColumn column1 = this.GetColumn(0);
      column1.Renderer = (IRenderer) renderer;
      column1.WordWrap = column1.WordWrap;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual TreeListView.TreeRenderer TreeColumnRenderer
    {
      get => this.treeRenderer ?? (this.treeRenderer = new TreeListView.TreeRenderer());
      set
      {
        this.treeRenderer = value ?? new TreeListView.TreeRenderer();
        this.EnsureTreeRendererPresent(this.treeRenderer);
      }
    }

    [Description("Should a wait cursor be shown when a branch is being expaned?")]
    [DefaultValue(true)]
    [Category("ObjectListView")]
    public virtual bool UseWaitCursorWhenExpanding
    {
      get => this.useWaitCursorWhenExpanding;
      set => this.useWaitCursorWhenExpanding = value;
    }

    protected TreeListView.Tree TreeModel
    {
      get => this.treeModel;
      set => this.treeModel = value;
    }

    public virtual bool IsExpanded(object model)
    {
      TreeListView.Branch branch = this.TreeModel.GetBranch(model);
      return branch != null && branch.IsExpanded;
    }

    public virtual void Collapse(object model)
    {
      if (this.GetItemCount() == 0)
        return;
      IList selectedObjects = this.SelectedObjects;
      int startIndex = this.TreeModel.Collapse(model);
      if (startIndex < 0)
        return;
      this.UpdateVirtualListSize();
      this.SelectedObjects = selectedObjects;
      this.RedrawItems(startIndex, this.GetItemCount() - 1, false);
    }

    public virtual void CollapseAll()
    {
      if (this.GetItemCount() == 0)
        return;
      IList selectedObjects = this.SelectedObjects;
      int startIndex = this.TreeModel.CollapseAll();
      if (startIndex < 0)
        return;
      this.UpdateVirtualListSize();
      this.SelectedObjects = selectedObjects;
      this.RedrawItems(startIndex, this.GetItemCount() - 1, false);
    }

    public override void ClearObjects()
    {
      if (this.InvokeRequired)
        this.Invoke((Delegate) new MethodInvoker(((ObjectListView) this).ClearObjects));
      else
        this.DiscardAllState();
    }

    public virtual void DiscardAllState()
    {
      this.CheckStateMap.Clear();
      this.RebuildAll(false);
    }

    public virtual void RebuildAll(bool preserveState)
    {
      int num = preserveState ? this.TopItemIndex : -1;
      this.RebuildAll(preserveState ? this.SelectedObjects : (IList) null, preserveState ? this.ExpandedObjects : (IEnumerable) null, preserveState ? this.CheckedObjects : (IList) null);
      if (!preserveState)
        return;
      this.TopItemIndex = num;
    }

    protected virtual void RebuildAll(IList selected, IEnumerable expanded, IList checkedObjects)
    {
      IEnumerable roots = this.Roots;
      TreeListView.CanExpandGetterDelegate canExpandGetter = this.CanExpandGetter;
      TreeListView.ChildrenGetterDelegate childrenGetter = this.ChildrenGetter;
      try
      {
        this.BeginUpdate();
        this.TreeModel = new TreeListView.Tree(this);
        this.VirtualListDataSource = (IVirtualListDataSource) this.TreeModel;
        this.CanExpandGetter = canExpandGetter;
        this.ChildrenGetter = childrenGetter;
        if (expanded != null)
          this.ExpandedObjects = expanded;
        this.Roots = roots;
        if (selected != null)
          this.SelectedObjects = selected;
        if (checkedObjects == null)
          return;
        this.CheckedObjects = checkedObjects;
      }
      finally
      {
        this.EndUpdate();
      }
    }

    public virtual void Expand(object model)
    {
      if (this.GetItemCount() == 0)
        return;
      IList selectedObjects = this.SelectedObjects;
      int startIndex = this.TreeModel.Expand(model);
      if (startIndex < 0)
        return;
      this.UpdateVirtualListSize();
      using (this.SuspendSelectionEventsDuring())
        this.SelectedObjects = selectedObjects;
      this.RedrawItems(startIndex, this.GetItemCount() - 1, false);
      if (!this.RevealAfterExpand || startIndex <= 0)
        return;
      this.BeginUpdate();
      try
      {
        int countPerPage = NativeMethods.GetCountPerPage((System.Windows.Forms.ListView) this);
        int visibleDescendentCount = this.TreeModel.GetVisibleDescendentCount(model);
        if (visibleDescendentCount < countPerPage)
          this.EnsureVisible(startIndex + visibleDescendentCount);
        else
          this.TopItemIndex = startIndex;
      }
      finally
      {
        this.EndUpdate();
      }
    }

    public virtual void ExpandAll()
    {
      if (this.GetItemCount() == 0)
        return;
      IList selectedObjects = this.SelectedObjects;
      int startIndex = this.TreeModel.ExpandAll();
      if (startIndex < 0)
        return;
      this.UpdateVirtualListSize();
      using (this.SuspendSelectionEventsDuring())
        this.SelectedObjects = selectedObjects;
      this.RedrawItems(startIndex, this.GetItemCount() - 1, false);
    }

    public override void RefreshObjects(IList modelObjects)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) (() => this.RefreshObjects(modelObjects)));
      }
      else
      {
        if (this.GetItemCount() == 0)
          return;
        IList selectedObjects = this.SelectedObjects;
        int num = int.MaxValue;
        foreach (object modelObject in (IEnumerable) modelObjects)
        {
          if (modelObject != null)
          {
            int val2 = this.TreeModel.RebuildChildren(modelObject);
            if (val2 >= 0)
              num = Math.Min(num, val2);
          }
        }
        if (num >= this.GetItemCount())
          return;
        this.ClearCachedInfo();
        this.UpdateVirtualListSize();
        this.SelectedObjects = selectedObjects;
        this.RedrawItems(num, this.GetItemCount() - 1, false);
      }
    }

    public virtual void ToggleExpansion(object model)
    {
      OLVListItem olvListItem = this.ModelToItem(model);
      if (this.IsExpanded(model))
      {
        TreeBranchCollapsingEventArgs e = new TreeBranchCollapsingEventArgs(model, olvListItem);
        this.OnCollapsing(e);
        if (e.Canceled)
          return;
        this.Collapse(model);
        this.OnCollapsed(new TreeBranchCollapsedEventArgs(model, olvListItem));
      }
      else
      {
        TreeBranchExpandingEventArgs e = new TreeBranchExpandingEventArgs(model, olvListItem);
        this.OnExpanding(e);
        if (e.Canceled)
          return;
        this.Expand(model);
        this.OnExpanded(new TreeBranchExpandedEventArgs(model, olvListItem));
      }
    }

    public virtual bool CanExpand(object model)
    {
      TreeListView.Branch branch = this.TreeModel.GetBranch(model);
      return branch != null && branch.CanExpand;
    }

    public virtual object GetParent(object model)
    {
      TreeListView.Branch branch = this.TreeModel.GetBranch(model);
      return branch == null || branch.ParentBranch == null ? (object) null : branch.ParentBranch.Model;
    }

    public virtual IEnumerable GetChildren(object model)
    {
      TreeListView.Branch branch = this.TreeModel.GetBranch(model);
      if (branch == null || !branch.CanExpand)
        return (IEnumerable) new ArrayList();
      if (!branch.IsExpanded)
        branch.Expand();
      return branch.Children;
    }

    protected override bool ProcessLButtonDown(OlvListViewHitTestInfo hti)
    {
      if (hti.HitTestLocation != HitTestLocation.ExpandButton)
        return base.ProcessLButtonDown(hti);
      this.PossibleFinishCellEditing();
      this.ToggleExpansion(hti.RowObject);
      return true;
    }

    public override OLVListItem MakeListViewItem(int itemIndex)
    {
      OLVListItem olvListItem = base.MakeListViewItem(itemIndex);
      TreeListView.Branch branch = this.TreeModel.GetBranch(olvListItem.RowObject);
      if (branch != null)
        olvListItem.IndentCount = branch.Level - 1;
      return olvListItem;
    }

    protected override void HandleApplicationIdle(object sender, EventArgs e)
    {
      base.HandleApplicationIdle(sender, e);
      this.Invalidate();
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData & Keys.KeyCode)
      {
        case Keys.Left:
        case Keys.Right:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (!(this.FocusedItem is OLVListItem focusedItem))
      {
        base.OnKeyDown(e);
      }
      else
      {
        object rowObject = focusedItem.RowObject;
        TreeListView.Branch branch = this.TreeModel.GetBranch(rowObject);
        switch (e.KeyCode)
        {
          case Keys.Left:
            if (branch.IsExpanded)
              this.Collapse(rowObject);
            else if (branch.ParentBranch != null && branch.ParentBranch.Model != null)
              this.SelectObject(branch.ParentBranch.Model, true);
            e.Handled = true;
            break;
          case Keys.Right:
            if (branch.IsExpanded)
            {
              List<TreeListView.Branch> filteredChildBranches = branch.FilteredChildBranches;
              if (filteredChildBranches.Count > 0)
                this.SelectObject(filteredChildBranches[0].Model, true);
            }
            else if (branch.CanExpand)
              this.Expand(rowObject);
            e.Handled = true;
            break;
        }
        base.OnKeyDown(e);
      }
    }

    public class TreeRenderer : HighlightTextRenderer
    {
      private Pen linePen;
      private bool isShowLines = true;
      public static int PIXELS_PER_LEVEL = 17;

      public TreeRenderer()
      {
        this.LinePen = new Pen(Color.Blue, 1f);
        this.LinePen.DashStyle = DashStyle.Dot;
      }

      private TreeListView.Branch Branch => this.TreeListView.TreeModel.GetBranch(this.RowObject);

      public Pen LinePen
      {
        get => this.linePen;
        set => this.linePen = value;
      }

      public TreeListView TreeListView => (TreeListView) this.ListView;

      public bool IsShowLines
      {
        get => this.isShowLines;
        set => this.isShowLines = value;
      }

      public override void Render(Graphics g, Rectangle r)
      {
        this.DrawBackground(g, r);
        TreeListView.Branch branch = this.Branch;
        Rectangle rectangle1 = this.ApplyCellPadding(r);
        Rectangle rectangle2 = rectangle1;
        rectangle2.Offset((branch.Level - 1) * TreeListView.TreeRenderer.PIXELS_PER_LEVEL, 0);
        rectangle2.Width = TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
        rectangle2.Height = TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
        rectangle2.Y = this.AlignVertically(rectangle1, rectangle2);
        int glyphMidVertical = rectangle2.Y + rectangle2.Height / 2;
        if (this.IsShowLines)
          this.DrawLines(g, r, this.LinePen, branch, glyphMidVertical);
        if (branch.CanExpand)
          this.DrawExpansionGlyph(g, rectangle2, branch.IsExpanded);
        int x = branch.Level * TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
        rectangle1.Offset(x, 0);
        rectangle1.Width -= x;
        this.DrawImageAndText(g, rectangle1);
      }

      protected virtual void DrawExpansionGlyph(Graphics g, Rectangle r, bool isExpanded)
      {
        if (this.UseStyles)
          this.DrawExpansionGlyphStyled(g, r, isExpanded);
        else
          this.DrawExpansionGlyphManual(g, r, isExpanded);
      }

      protected virtual bool UseStyles => !this.IsPrinting && Application.RenderWithVisualStyles;

      protected virtual void DrawExpansionGlyphStyled(Graphics g, Rectangle r, bool isExpanded)
      {
        VisualStyleElement element = VisualStyleElement.TreeView.Glyph.Closed;
        if (isExpanded)
          element = VisualStyleElement.TreeView.Glyph.Opened;
        new VisualStyleRenderer(element).DrawBackground((IDeviceContext) g, r);
      }

      protected virtual void DrawExpansionGlyphManual(Graphics g, Rectangle r, bool isExpanded)
      {
        int height = 8;
        int width = 8;
        int x = r.X + 4;
        int y = r.Y + r.Height / 2 - 4;
        g.DrawRectangle(new Pen(SystemBrushes.ControlDark), x, y, width, height);
        g.FillRectangle(Brushes.White, x + 1, y + 1, width - 1, height - 1);
        g.DrawLine(Pens.Black, x + 2, y + 4, x + width - 2, y + 4);
        if (isExpanded)
          return;
        g.DrawLine(Pens.Black, x + 4, y + 2, x + 4, y + height - 2);
      }

      protected virtual void DrawLines(
        Graphics g,
        Rectangle r,
        Pen p,
        TreeListView.Branch br,
        int glyphMidVertical)
      {
        Rectangle rectangle = r with
        {
          Width = TreeListView.TreeRenderer.PIXELS_PER_LEVEL
        };
        int top = rectangle.Top;
        foreach (TreeListView.Branch ancestor in (IEnumerable<TreeListView.Branch>) br.Ancestors)
        {
          if (!ancestor.IsLastChild && !ancestor.IsOnlyBranch)
          {
            int num = rectangle.Left + rectangle.Width / 2;
            g.DrawLine(p, num, top, num, rectangle.Bottom);
          }
          rectangle.Offset(TreeListView.TreeRenderer.PIXELS_PER_LEVEL, 0);
        }
        int num1 = rectangle.Left + rectangle.Width / 2;
        g.DrawLine(p, num1, glyphMidVertical, rectangle.Right, glyphMidVertical);
        if (br.IsFirstBranch)
        {
          if (br.IsLastChild || br.IsOnlyBranch)
            return;
          g.DrawLine(p, num1, glyphMidVertical, num1, rectangle.Bottom);
        }
        else if (br.IsLastChild)
          g.DrawLine(p, num1, top, num1, glyphMidVertical);
        else
          g.DrawLine(p, num1, top, num1, rectangle.Bottom);
      }

      protected override void HandleHitTest(Graphics g, OlvListViewHitTestInfo hti, int x, int y)
      {
        TreeListView.Branch branch = this.Branch;
        Rectangle bounds = this.Bounds;
        if (branch.CanExpand)
        {
          bounds.Offset((branch.Level - 1) * TreeListView.TreeRenderer.PIXELS_PER_LEVEL, 0);
          bounds.Width = TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
          if (bounds.Contains(x, y))
          {
            hti.HitTestLocation = HitTestLocation.ExpandButton;
            return;
          }
        }
        bounds = this.Bounds;
        int num = branch.Level * TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
        bounds.X += num;
        bounds.Width -= num;
        if (x < bounds.Left)
          hti.HitTestLocation = HitTestLocation.Nothing;
        else
          this.StandardHitTest(g, hti, bounds, x, y);
      }

      protected override Rectangle HandleGetEditRectangle(
        Graphics g,
        Rectangle cellBounds,
        OLVListItem item,
        int subItemIndex,
        Size preferredSize)
      {
        return this.StandardGetEditRectangle(g, cellBounds, preferredSize);
      }
    }

    public delegate bool CanExpandGetterDelegate(object model);

    public delegate IEnumerable ChildrenGetterDelegate(object model);

    public class Tree : IVirtualListDataSource, IFilterableDataSource
    {
      private TreeListView.CanExpandGetterDelegate canExpandGetter;
      private TreeListView.ChildrenGetterDelegate childrenGetter;
      private OLVColumn lastSortColumn;
      private SortOrder lastSortOrder;
      private Dictionary<object, TreeListView.Branch> mapObjectToBranch = new Dictionary<object, TreeListView.Branch>();
      internal Dictionary<object, bool> mapObjectToExpanded = new Dictionary<object, bool>();
      private Dictionary<object, int> mapObjectToIndex = new Dictionary<object, int>();
      private ArrayList objectList = new ArrayList();
      private TreeListView treeView;
      private TreeListView.Branch trunk;
      protected IModelFilter modelFilter;
      protected IListFilter listFilter;

      public Tree(TreeListView treeView)
      {
        this.treeView = treeView;
        this.trunk = new TreeListView.Branch((TreeListView.Branch) null, this, (object) null);
        this.trunk.IsExpanded = true;
      }

      public TreeListView.CanExpandGetterDelegate CanExpandGetter
      {
        get => this.canExpandGetter;
        set => this.canExpandGetter = value;
      }

      public TreeListView.ChildrenGetterDelegate ChildrenGetter
      {
        get => this.childrenGetter;
        set => this.childrenGetter = value;
      }

      public IEnumerable RootObjects
      {
        get => this.trunk.Children;
        set
        {
          this.trunk.Children = value;
          foreach (TreeListView.Branch childBranch in this.trunk.ChildBranches)
            childBranch.RefreshChildren();
          this.RebuildList();
        }
      }

      public TreeListView TreeView => this.treeView;

      public virtual int Collapse(object model)
      {
        TreeListView.Branch branch = this.GetBranch(model);
        if (branch == null || !branch.IsExpanded)
          return -1;
        if (!branch.Visible)
        {
          branch.Collapse();
          return -1;
        }
        int visibleDescendents = branch.NumberVisibleDescendents;
        branch.Collapse();
        int objectIndex = this.GetObjectIndex(model);
        this.objectList.RemoveRange(objectIndex + 1, visibleDescendents);
        this.RebuildObjectMap(objectIndex + 1);
        return objectIndex;
      }

      public virtual int CollapseAll()
      {
        foreach (TreeListView.Branch childBranch in this.trunk.ChildBranches)
        {
          if (childBranch.IsExpanded)
            childBranch.Collapse();
        }
        this.RebuildList();
        return 0;
      }

      public virtual int Expand(object model)
      {
        TreeListView.Branch branch = this.GetBranch(model);
        if (branch == null || !branch.CanExpand || branch.IsExpanded)
          return -1;
        if (!branch.Visible)
        {
          branch.Expand();
          return -1;
        }
        int objectIndex = this.GetObjectIndex(model);
        this.InsertChildren(branch, objectIndex + 1);
        return objectIndex;
      }

      public virtual int ExpandAll()
      {
        this.trunk.ExpandAll();
        this.Sort(this.lastSortColumn, this.lastSortOrder);
        return 0;
      }

      public virtual TreeListView.Branch GetBranch(object model)
      {
        if (model == null)
          return (TreeListView.Branch) null;
        TreeListView.Branch branch;
        this.mapObjectToBranch.TryGetValue(model, out branch);
        return branch;
      }

      public virtual int GetVisibleDescendentCount(object model)
      {
        TreeListView.Branch branch = this.GetBranch(model);
        return branch == null || !branch.IsExpanded ? 0 : branch.NumberVisibleDescendents;
      }

      public virtual int RebuildChildren(object model)
      {
        TreeListView.Branch branch = this.GetBranch(model);
        if (branch == null || !branch.Visible || !branch.CanExpand)
          return -1;
        int visibleDescendents = branch.NumberVisibleDescendents;
        branch.ClearCachedInfo();
        int objectIndex = this.GetObjectIndex(model);
        if (visibleDescendents > 0)
          this.objectList.RemoveRange(objectIndex + 1, visibleDescendents);
        if (branch.CanExpand && branch.IsExpanded)
        {
          branch.FetchChildren();
          this.InsertChildren(branch, objectIndex + 1);
        }
        return objectIndex;
      }

      internal bool IsModelExpanded(object model)
      {
        if (model == null)
          return true;
        bool flag = false;
        this.mapObjectToExpanded.TryGetValue(model, out flag);
        return flag;
      }

      internal void SetModelExpanded(object model, bool isExpanded)
      {
        if (model == null)
          return;
        if (isExpanded)
          this.mapObjectToExpanded[model] = true;
        else
          this.mapObjectToExpanded.Remove(model);
      }

      protected virtual void InsertChildren(TreeListView.Branch br, int index)
      {
        br.Expand();
        br.Sort(this.GetBranchComparer());
        this.objectList.InsertRange(index, (ICollection) br.Flatten());
        this.RebuildObjectMap(index);
      }

      protected virtual void RebuildList()
      {
        this.objectList = ArrayList.Adapter(this.trunk.Flatten());
        List<TreeListView.Branch> filteredChildBranches = this.trunk.FilteredChildBranches;
        if (filteredChildBranches.Count > 0)
        {
          filteredChildBranches[0].IsFirstBranch = true;
          filteredChildBranches[0].IsOnlyBranch = filteredChildBranches.Count == 1;
        }
        this.RebuildObjectMap(0);
      }

      protected virtual void RebuildObjectMap(int startIndex)
      {
        if (startIndex == 0)
          this.mapObjectToIndex.Clear();
        for (int index = startIndex; index < this.objectList.Count; ++index)
          this.mapObjectToIndex[this.objectList[index]] = index;
      }

      internal TreeListView.Branch MakeBranch(TreeListView.Branch parent, object model)
      {
        TreeListView.Branch branch = new TreeListView.Branch(parent, this, model);
        this.mapObjectToBranch[model] = branch;
        return branch;
      }

      public virtual object GetNthObject(int n) => this.objectList[n];

      public virtual int GetObjectCount() => this.trunk.NumberVisibleDescendents;

      public virtual int GetObjectIndex(object model)
      {
        int num;
        return model != null && this.mapObjectToIndex.TryGetValue(model, out num) ? num : -1;
      }

      public virtual void PrepareCache(int first, int last)
      {
      }

      public virtual int SearchText(string value, int first, int last, OLVColumn column)
      {
        return AbstractVirtualListDataSource.DefaultSearchText(value, first, last, column, (IVirtualListDataSource) this);
      }

      public virtual void Sort(OLVColumn column, SortOrder order)
      {
        this.lastSortColumn = column;
        this.lastSortOrder = order;
        foreach (TreeListView.Branch childBranch in this.trunk.ChildBranches)
          childBranch.IsFirstBranch = false;
        this.trunk.Sort(this.GetBranchComparer());
        this.RebuildList();
      }

      protected virtual TreeListView.BranchComparer GetBranchComparer()
      {
        return this.lastSortColumn == null ? (TreeListView.BranchComparer) null : new TreeListView.BranchComparer((IComparer) new ModelObjectComparer(this.lastSortColumn, this.lastSortOrder, this.treeView.GetColumn(0), this.lastSortOrder));
      }

      public virtual void AddObjects(ICollection modelObjects)
      {
        ArrayList collection = new ArrayList();
        foreach (object root in this.treeView.Roots)
          collection.Add(root);
        foreach (object modelObject in (IEnumerable) modelObjects)
          collection.Add(modelObject);
        this.SetObjects((IEnumerable) collection);
      }

      public virtual void RemoveObjects(ICollection modelObjects)
      {
        ArrayList collection = new ArrayList();
        foreach (object root in this.treeView.Roots)
          collection.Add(root);
        foreach (object modelObject in (IEnumerable) modelObjects)
        {
          collection.Remove(modelObject);
          this.mapObjectToIndex.Remove(modelObject);
        }
        this.SetObjects((IEnumerable) collection);
      }

      public virtual void SetObjects(IEnumerable collection) => this.treeView.Roots = collection;

      public void ApplyFilters(IModelFilter modelFilter, IListFilter listFilter)
      {
        this.modelFilter = modelFilter;
        this.listFilter = listFilter;
        this.RebuildList();
      }

      internal bool IsFiltering
      {
        get
        {
          if (!this.treeView.UseFiltering)
            return false;
          return this.modelFilter != null || this.listFilter != null;
        }
      }

      internal bool IncludeModel(object model)
      {
        return !this.treeView.UseFiltering || this.modelFilter == null || this.modelFilter.Filter(model);
      }
    }

    public class Branch
    {
      private List<TreeListView.Branch> childBranches = new List<TreeListView.Branch>();
      private object model;
      private TreeListView.Branch parentBranch;
      private TreeListView.Tree tree;
      private bool alreadyHasChildren;
      private TreeListView.Branch.BranchFlags flags;

      public Branch(TreeListView.Branch parent, TreeListView.Tree tree, object model)
      {
        this.ParentBranch = parent;
        this.Tree = tree;
        this.Model = model;
      }

      public virtual IList<TreeListView.Branch> Ancestors
      {
        get
        {
          List<TreeListView.Branch> list = new List<TreeListView.Branch>();
          if (this.ParentBranch != null)
            this.ParentBranch.PushAncestors((IList<TreeListView.Branch>) list);
          return (IList<TreeListView.Branch>) list;
        }
      }

      private void PushAncestors(IList<TreeListView.Branch> list)
      {
        if (this.ParentBranch == null)
          return;
        this.ParentBranch.PushAncestors(list);
        list.Add(this);
      }

      public virtual bool CanExpand
      {
        get
        {
          return this.Tree.CanExpandGetter != null && this.Model != null && this.Tree.CanExpandGetter(this.Model);
        }
      }

      public List<TreeListView.Branch> ChildBranches
      {
        get => this.childBranches;
        set => this.childBranches = value;
      }

      public virtual IEnumerable Children
      {
        get
        {
          ArrayList children = new ArrayList();
          foreach (TreeListView.Branch childBranch in this.ChildBranches)
            children.Add(childBranch.Model);
          return (IEnumerable) children;
        }
        set
        {
          this.ChildBranches.Clear();
          foreach (object model in value)
            this.AddChild(model);
        }
      }

      private void AddChild(object model)
      {
        TreeListView.Branch branch = this.Tree.GetBranch(model);
        if (branch == null)
          branch = this.Tree.MakeBranch(this, model);
        else
          branch.ParentBranch = this;
        this.ChildBranches.Add(branch);
      }

      public List<TreeListView.Branch> FilteredChildBranches
      {
        get
        {
          if (!this.IsExpanded)
            return new List<TreeListView.Branch>();
          if (!this.Tree.IsFiltering)
            return this.ChildBranches;
          List<TreeListView.Branch> filteredChildBranches = new List<TreeListView.Branch>();
          foreach (TreeListView.Branch childBranch in this.ChildBranches)
          {
            if (this.Tree.IncludeModel(childBranch.Model))
              filteredChildBranches.Add(childBranch);
            else if (childBranch.FilteredChildBranches.Count > 0)
              filteredChildBranches.Add(childBranch);
          }
          return filteredChildBranches;
        }
      }

      public bool IsExpanded
      {
        get => this.Tree.IsModelExpanded(this.Model);
        set => this.Tree.SetModelExpanded(this.Model, value);
      }

      public virtual bool IsFirstBranch
      {
        get
        {
          return (this.flags & TreeListView.Branch.BranchFlags.FirstBranch) != (TreeListView.Branch.BranchFlags) 0;
        }
        set
        {
          if (value)
            this.flags |= TreeListView.Branch.BranchFlags.FirstBranch;
          else
            this.flags &= ~TreeListView.Branch.BranchFlags.FirstBranch;
        }
      }

      public virtual bool IsLastChild
      {
        get
        {
          return (this.flags & TreeListView.Branch.BranchFlags.LastChild) != (TreeListView.Branch.BranchFlags) 0;
        }
        set
        {
          if (value)
            this.flags |= TreeListView.Branch.BranchFlags.LastChild;
          else
            this.flags &= ~TreeListView.Branch.BranchFlags.LastChild;
        }
      }

      public virtual bool IsOnlyBranch
      {
        get
        {
          return (this.flags & TreeListView.Branch.BranchFlags.OnlyBranch) != (TreeListView.Branch.BranchFlags) 0;
        }
        set
        {
          if (value)
            this.flags |= TreeListView.Branch.BranchFlags.OnlyBranch;
          else
            this.flags &= ~TreeListView.Branch.BranchFlags.OnlyBranch;
        }
      }

      public int Level => this.ParentBranch == null ? 0 : this.ParentBranch.Level + 1;

      public object Model
      {
        get => this.model;
        set => this.model = value;
      }

      public virtual int NumberVisibleDescendents
      {
        get
        {
          if (!this.IsExpanded)
            return 0;
          List<TreeListView.Branch> filteredChildBranches = this.FilteredChildBranches;
          int count = filteredChildBranches.Count;
          foreach (TreeListView.Branch branch in filteredChildBranches)
            count += branch.NumberVisibleDescendents;
          return count;
        }
      }

      public TreeListView.Branch ParentBranch
      {
        get => this.parentBranch;
        set => this.parentBranch = value;
      }

      public TreeListView.Tree Tree
      {
        get => this.tree;
        set => this.tree = value;
      }

      public virtual bool Visible
      {
        get
        {
          if (this.ParentBranch == null)
            return true;
          return this.ParentBranch.IsExpanded && this.ParentBranch.Visible;
        }
      }

      public virtual void ClearCachedInfo()
      {
        this.Children = (IEnumerable) new ArrayList();
        this.alreadyHasChildren = false;
      }

      public virtual void Collapse() => this.IsExpanded = false;

      public virtual void Expand()
      {
        if (!this.CanExpand)
          return;
        this.IsExpanded = true;
        this.FetchChildren();
      }

      public virtual void ExpandAll()
      {
        this.Expand();
        foreach (TreeListView.Branch childBranch in this.ChildBranches)
        {
          if (childBranch.CanExpand)
            childBranch.ExpandAll();
        }
      }

      public virtual void FetchChildren()
      {
        if (this.alreadyHasChildren)
          return;
        this.alreadyHasChildren = true;
        if (this.Tree.ChildrenGetter == null)
          return;
        if (this.Tree.TreeView.UseWaitCursorWhenExpanding)
        {
          Cursor current = Cursor.Current;
          try
          {
            Cursor.Current = Cursors.WaitCursor;
            this.Children = this.Tree.ChildrenGetter(this.Model);
          }
          finally
          {
            Cursor.Current = current;
          }
        }
        else
          this.Children = this.Tree.ChildrenGetter(this.Model);
      }

      public virtual IList Flatten()
      {
        ArrayList flatList = new ArrayList();
        if (this.IsExpanded)
          this.FlattenOnto((IList) flatList);
        return (IList) flatList;
      }

      public virtual void FlattenOnto(IList flatList)
      {
        TreeListView.Branch branch = (TreeListView.Branch) null;
        foreach (TreeListView.Branch filteredChildBranch in this.FilteredChildBranches)
        {
          branch = filteredChildBranch;
          filteredChildBranch.IsLastChild = false;
          flatList.Add(filteredChildBranch.Model);
          if (filteredChildBranch.IsExpanded)
            filteredChildBranch.FlattenOnto(flatList);
        }
        if (branch == null)
          return;
        branch.IsLastChild = true;
      }

      public virtual void RefreshChildren()
      {
        if (!this.IsExpanded || !this.CanExpand)
          return;
        this.FetchChildren();
        foreach (TreeListView.Branch childBranch in this.ChildBranches)
          childBranch.RefreshChildren();
      }

      public virtual void Sort(TreeListView.BranchComparer comparer)
      {
        if (this.ChildBranches.Count == 0)
          return;
        if (comparer != null)
          this.ChildBranches.Sort((IComparer<TreeListView.Branch>) comparer);
        foreach (TreeListView.Branch childBranch in this.ChildBranches)
          childBranch.Sort(comparer);
      }

      [Flags]
      public enum BranchFlags
      {
        FirstBranch = 1,
        LastChild = 2,
        OnlyBranch = 4,
      }
    }

    public class BranchComparer : IComparer<TreeListView.Branch>
    {
      private IComparer actualComparer;

      public BranchComparer(IComparer actualComparer) => this.actualComparer = actualComparer;

      public int Compare(TreeListView.Branch x, TreeListView.Branch y)
      {
        return this.actualComparer.Compare(x.Model, y.Model);
      }
    }
  }
}
