﻿// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.Design.ObjectListViewDesigner
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace BrightIdeasSoftware.Design
{
  public class ObjectListViewDesigner : ControlDesigner
  {
    protected ControlDesigner listViewDesigner;
    protected IDesignerFilter designerFilter;
    protected MethodInfo listViewDesignGetHitTest;
    protected MethodInfo listViewDesignWndProc;

    public override void Initialize(IComponent component)
    {
      System.Type type1 = System.Type.GetType("System.Windows.Forms.Design.ListViewDesigner, System.Design");
      if ((object) type1 == null)
        type1 = System.Type.GetType("System.Windows.Forms.Design.ListViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
      System.Type type2 = type1;
      this.listViewDesigner = (object) type2 != null ? (ControlDesigner) Activator.CreateInstance(type2, BindingFlags.Instance | BindingFlags.Public, (Binder) null, (object[]) null, (CultureInfo) null) : throw new ArgumentException("Could not load ListViewDesigner");
      this.designerFilter = (IDesignerFilter) this.listViewDesigner;
      this.listViewDesignGetHitTest = type2.GetMethod("GetHitTest", BindingFlags.Instance | BindingFlags.NonPublic);
      this.listViewDesignWndProc = type2.GetMethod("WndProc", BindingFlags.Instance | BindingFlags.NonPublic);
      TypeDescriptor.CreateAssociation((object) component, (object) this.listViewDesigner);
      IServiceContainer site = (IServiceContainer) component.Site;
      if (site != null && this.GetService(typeof (DesignerCommandSet)) == null)
        site.AddService(typeof (DesignerCommandSet), (object) new ObjectListViewDesigner.CDDesignerCommandSet((ComponentDesigner) this));
      this.listViewDesigner.Initialize(component);
      base.Initialize(component);
      this.RemoveDuplicateDockingActionList();
    }

    public override void InitializeNewComponent(IDictionary defaultValues)
    {
      base.InitializeNewComponent(defaultValues);
      this.listViewDesigner.InitializeNewComponent(defaultValues);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.listViewDesigner != null)
        this.listViewDesigner.Dispose();
      base.Dispose(disposing);
    }

    private void RemoveDuplicateDockingActionList()
    {
      FieldInfo field = typeof (ControlDesigner).GetField("dockingAction", BindingFlags.Instance | BindingFlags.NonPublic);
      if ((object) field == null)
        return;
      DesignerActionList actionList = (DesignerActionList) field.GetValue((object) this);
      if (actionList == null)
        return;
      ((DesignerActionService) this.GetService(typeof (DesignerActionService)))?.Remove((IComponent) this.Control, actionList);
    }

    protected override void PreFilterProperties(IDictionary properties)
    {
      base.PreFilterProperties(properties);
      this.designerFilter.PreFilterProperties(properties);
      List<string> stringList = new List<string>((IEnumerable<string>) new string[7]
      {
        "BackgroundImage",
        "BackgroundImageTiled",
        "HotTracking",
        "HoverSelection",
        "LabelEdit",
        "VirtualListSize",
        "VirtualMode"
      });
      foreach (string key in (IEnumerable) properties.Keys)
      {
        if (key.StartsWith("ToolTip"))
          stringList.Add(key);
      }
      if (this.Control is TreeListView)
        stringList.AddRange((IEnumerable<string>) new string[8]
        {
          "GroupImageList",
          "GroupWithItemCountFormat",
          "GroupWithItemCountSingularFormat",
          "HasCollapsibleGroups",
          "SpaceBetweenGroups",
          "ShowGroups",
          "SortGroupItemsByPrimaryColumn",
          "ShowItemCountOnGroups"
        });
      foreach (string key in stringList)
      {
        PropertyDescriptor property = TypeDescriptor.CreateProperty(typeof (ObjectListView), (PropertyDescriptor) properties[(object) key], (Attribute) new BrowsableAttribute(false));
        properties[(object) key] = (object) property;
      }
    }

    protected override void PreFilterEvents(IDictionary events)
    {
      base.PreFilterEvents(events);
      this.designerFilter.PreFilterEvents(events);
      List<string> stringList = new List<string>((IEnumerable<string>) new string[8]
      {
        "AfterLabelEdit",
        "BeforeLabelEdit",
        "DrawColumnHeader",
        "DrawItem",
        "DrawSubItem",
        "RetrieveVirtualItem",
        "SearchForVirtualItem",
        "VirtualItemsSelectionRangeChanged"
      });
      if (this.Control is TreeListView)
        stringList.AddRange((IEnumerable<string>) new string[6]
        {
          "AboutToCreateGroups",
          "AfterCreatingGroups",
          "BeforeCreatingGroups",
          "GroupTaskClicked",
          "GroupExpandingCollapsing",
          "GroupStateChanged"
        });
      foreach (string key in stringList)
      {
        EventDescriptor eventDescriptor = TypeDescriptor.CreateEvent(typeof (ObjectListView), (EventDescriptor) events[(object) key], (Attribute) new BrowsableAttribute(false));
        events[(object) key] = (object) eventDescriptor;
      }
    }

    protected override void PostFilterAttributes(IDictionary attributes)
    {
      this.designerFilter.PostFilterAttributes(attributes);
      base.PostFilterAttributes(attributes);
    }

    protected override void PostFilterEvents(IDictionary events)
    {
      this.designerFilter.PostFilterEvents(events);
      base.PostFilterEvents(events);
    }

    public override DesignerActionListCollection ActionLists
    {
      get
      {
        DesignerActionListCollection actionLists = this.listViewDesigner.ActionLists;
        if (actionLists.Count > 0 && !(actionLists[0] is ObjectListViewDesigner.ListViewActionListAdapter))
          actionLists[0] = (DesignerActionList) new ObjectListViewDesigner.ListViewActionListAdapter(this, actionLists[0]);
        return actionLists;
      }
    }

    public override ICollection AssociatedComponents
    {
      get
      {
        ArrayList associatedComponents = new ArrayList(base.AssociatedComponents);
        associatedComponents.AddRange(this.listViewDesigner.AssociatedComponents);
        return (ICollection) associatedComponents;
      }
    }

    protected override bool GetHitTest(Point point)
    {
      return (bool) this.listViewDesignGetHitTest.Invoke((object) this.listViewDesigner, new object[1]
      {
        (object) point
      });
    }

    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 78:
        case 8270:
          this.listViewDesignWndProc.Invoke((object) this.listViewDesigner, new object[1]
          {
            (object) m
          });
          break;
        default:
          base.WndProc(ref m);
          break;
      }
    }

    private class ListViewActionListAdapter : DesignerActionList
    {
      private ObjectListViewDesigner designer;
      private DesignerActionList wrappedList;

      public ListViewActionListAdapter(
        ObjectListViewDesigner designer,
        DesignerActionList wrappedList)
        : base(wrappedList.Component)
      {
        this.designer = designer;
        this.wrappedList = wrappedList;
      }

      public override DesignerActionItemCollection GetSortedActionItems()
      {
        DesignerActionItemCollection sortedActionItems = this.wrappedList.GetSortedActionItems();
        sortedActionItems.RemoveAt(2);
        sortedActionItems.RemoveAt(0);
        return sortedActionItems;
      }

      private void EditValue(
        ComponentDesigner componentDesigner,
        IComponent iComponent,
        string propertyName)
      {
        System.Type.GetType("System.Windows.Forms.Design.EditorServiceContext, System.Design").InvokeMember(nameof (EditValue), BindingFlags.Static | BindingFlags.InvokeMethod, (Binder) null, (object) null, new object[3]
        {
          (object) componentDesigner,
          (object) iComponent,
          (object) propertyName
        });
      }

      private void SetValue(object target, string propertyName, object value)
      {
        TypeDescriptor.GetProperties(target)[propertyName].SetValue(target, value);
      }

      public void InvokeColumnsDialog()
      {
        this.EditValue((ComponentDesigner) this.designer, this.Component, "Columns");
      }

      public ImageList LargeImageList
      {
        get => ((ListView) this.Component).LargeImageList;
        set => this.SetValue((object) this.Component, nameof (LargeImageList), (object) value);
      }

      public ImageList SmallImageList
      {
        get => ((ListView) this.Component).SmallImageList;
        set => this.SetValue((object) this.Component, nameof (SmallImageList), (object) value);
      }

      public View View
      {
        get => ((ListView) this.Component).View;
        set => this.SetValue((object) this.Component, nameof (View), (object) value);
      }
    }

    private new class CDDesignerCommandSet : DesignerCommandSet
    {
      private readonly ComponentDesigner componentDesigner;

      public CDDesignerCommandSet(ComponentDesigner componentDesigner)
      {
        this.componentDesigner = componentDesigner;
      }

      public override ICollection GetCommands(string name)
      {
        if (this.componentDesigner != null)
        {
          switch (name)
          {
            case "Verbs":
              return (ICollection) this.componentDesigner.Verbs;
            case "ActionLists":
              return (ICollection) this.componentDesigner.ActionLists;
          }
        }
        return base.GetCommands(name);
      }
    }
  }
}
