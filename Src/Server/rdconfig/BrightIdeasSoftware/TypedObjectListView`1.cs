// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.TypedObjectListView`1
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace BrightIdeasSoftware
{
  public class TypedObjectListView<T> where T : class
  {
    private ObjectListView olv;
    private TypedObjectListView<T>.TypedCheckStateGetterDelegate checkStateGetter;
    private TypedObjectListView<T>.TypedCheckStatePutterDelegate checkStatePutter;

    public TypedObjectListView(ObjectListView olv) => this.olv = olv;

    public virtual T CheckedObject => (T) this.olv.CheckedObject;

    public virtual IList<T> CheckedObjects
    {
      get
      {
        IList checkedObjects1 = this.olv.CheckedObjects;
        List<T> checkedObjects2 = new List<T>(checkedObjects1.Count);
        foreach (object obj in (IEnumerable) checkedObjects1)
          checkedObjects2.Add((T) obj);
        return (IList<T>) checkedObjects2;
      }
      set => this.olv.CheckedObjects = (IList) value;
    }

    public virtual ObjectListView ListView
    {
      get => this.olv;
      set => this.olv = value;
    }

    public virtual IList<T> Objects
    {
      get
      {
        List<T> objects = new List<T>(this.olv.GetItemCount());
        for (int index = 0; index < this.olv.GetItemCount(); ++index)
          objects.Add(this.GetModelObject(index));
        return (IList<T>) objects;
      }
      set => this.olv.SetObjects((IEnumerable) value);
    }

    public virtual T SelectedObject
    {
      get => (T) this.olv.SelectedObject;
      set => this.olv.SelectedObject = (object) value;
    }

    public virtual IList<T> SelectedObjects
    {
      get
      {
        List<T> selectedObjects = new List<T>(this.olv.SelectedIndices.Count);
        foreach (int selectedIndex in this.olv.SelectedIndices)
          selectedObjects.Add((T) this.olv.GetModelObject(selectedIndex));
        return (IList<T>) selectedObjects;
      }
      set => this.olv.SelectedObjects = (IList) value;
    }

    public virtual TypedColumn<T> GetColumn(int i) => new TypedColumn<T>(this.olv.GetColumn(i));

    public virtual TypedColumn<T> GetColumn(string name)
    {
      return new TypedColumn<T>(this.olv.GetColumn(name));
    }

    public virtual T GetModelObject(int index) => (T) this.olv.GetModelObject(index);

    public virtual TypedObjectListView<T>.TypedCheckStateGetterDelegate CheckStateGetter
    {
      get => this.checkStateGetter;
      set
      {
        this.checkStateGetter = value;
        if (value == null)
          this.olv.CheckStateGetter = (CheckStateGetterDelegate) null;
        else
          this.olv.CheckStateGetter = (CheckStateGetterDelegate) (x => this.checkStateGetter((T) x));
      }
    }

    public virtual TypedObjectListView<T>.TypedBooleanCheckStateGetterDelegate BooleanCheckStateGetter
    {
      set
      {
        if (value == null)
          this.olv.BooleanCheckStateGetter = (BooleanCheckStateGetterDelegate) null;
        else
          this.olv.BooleanCheckStateGetter = (BooleanCheckStateGetterDelegate) (x => value((T) x));
      }
    }

    public virtual TypedObjectListView<T>.TypedCheckStatePutterDelegate CheckStatePutter
    {
      get => this.checkStatePutter;
      set
      {
        this.checkStatePutter = value;
        if (value == null)
          this.olv.CheckStatePutter = (CheckStatePutterDelegate) null;
        else
          this.olv.CheckStatePutter = (CheckStatePutterDelegate) ((x, newValue) => this.checkStatePutter((T) x, newValue));
      }
    }

    public virtual TypedObjectListView<T>.TypedBooleanCheckStatePutterDelegate BooleanCheckStatePutter
    {
      set
      {
        if (value == null)
          this.olv.BooleanCheckStatePutter = (BooleanCheckStatePutterDelegate) null;
        else
          this.olv.BooleanCheckStatePutter = (BooleanCheckStatePutterDelegate) ((x, newValue) => value((T) x, newValue));
      }
    }

    public virtual TypedObjectListView<T>.TypedCellToolTipGetterDelegate CellToolTipGetter
    {
      set
      {
        if (value == null)
          this.olv.CellToolTipGetter = (CellToolTipGetterDelegate) null;
        else
          this.olv.CellToolTipGetter = (CellToolTipGetterDelegate) ((col, x) => value(col, (T) x));
      }
    }

    public virtual HeaderToolTipGetterDelegate HeaderToolTipGetter
    {
      get => this.olv.HeaderToolTipGetter;
      set => this.olv.HeaderToolTipGetter = value;
    }

    public virtual void GenerateAspectGetters()
    {
      for (int i = 0; i < this.ListView.Columns.Count; ++i)
        this.GetColumn(i).GenerateAspectGetter();
    }

        public delegate CheckState TypedCheckStateGetterDelegate(T rowObject);// where T : class;

        public delegate bool TypedBooleanCheckStateGetterDelegate(T rowObject);// where T : class;

        public delegate CheckState TypedCheckStatePutterDelegate(T rowObject, CheckState newValue);// where T : class;

        public delegate bool TypedBooleanCheckStatePutterDelegate(T rowObject, bool newValue);// where T : class;

        public delegate string TypedCellToolTipGetterDelegate(OLVColumn column, T modelObject);// where T : class;
  }
}
