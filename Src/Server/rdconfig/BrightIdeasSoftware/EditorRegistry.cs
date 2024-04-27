// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.EditorRegistry
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public class EditorRegistry
  {
    private EditorCreatorDelegate firstChanceCreator;
    private EditorCreatorDelegate defaultCreator;
    private Dictionary<System.Type, EditorCreatorDelegate> creatorMap = new Dictionary<System.Type, EditorCreatorDelegate>();

    public EditorRegistry() => this.InitializeStandardTypes();

    private void InitializeStandardTypes()
    {
      this.Register(typeof (bool), typeof (BooleanCellEditor));
      this.Register(typeof (short), typeof (IntUpDown));
      this.Register(typeof (int), typeof (IntUpDown));
      this.Register(typeof (long), typeof (IntUpDown));
      this.Register(typeof (ushort), typeof (UintUpDown));
      this.Register(typeof (uint), typeof (UintUpDown));
      this.Register(typeof (ulong), typeof (UintUpDown));
      this.Register(typeof (float), typeof (FloatCellEditor));
      this.Register(typeof (double), typeof (FloatCellEditor));
      this.Register(typeof (DateTime), (EditorCreatorDelegate) ((model, column, value) => (Control) new DateTimePicker()
      {
        Format = DateTimePickerFormat.Short
      }));
      this.Register(typeof (bool), (EditorCreatorDelegate) ((model, column, value) =>
      {
        CheckBox checkBox = (CheckBox) new BooleanCellEditor2();
        checkBox.ThreeState = column.TriStateCheckBoxes;
        return (Control) checkBox;
      }));
    }

    public void Register(System.Type type, System.Type controlType)
    {
      this.Register(type, (EditorCreatorDelegate) ((model, column, value) => controlType.InvokeMember("", BindingFlags.CreateInstance, (Binder) null, (object) null, (object[]) null) as Control));
    }

    public void Register(System.Type type, EditorCreatorDelegate creator)
    {
      this.creatorMap[type] = creator;
    }

    public void RegisterDefault(EditorCreatorDelegate creator) => this.defaultCreator = creator;

    public void RegisterFirstChance(EditorCreatorDelegate creator)
    {
      this.firstChanceCreator = creator;
    }

    public Control GetEditor(object model, OLVColumn column, object value)
    {
      if (this.firstChanceCreator != null)
      {
        Control editor = this.firstChanceCreator(model, column, value);
        if (editor != null)
          return editor;
      }
      System.Type key = value == null ? column.DataType : value.GetType();
      if ((object) key != null && this.creatorMap.ContainsKey(key))
      {
        Control editor = this.creatorMap[key](model, column, value);
        if (editor != null)
          return editor;
      }
      if (value != null && value.GetType().IsEnum)
        return this.CreateEnumEditor(value.GetType());
      return this.defaultCreator != null ? this.defaultCreator(model, column, value) : (Control) null;
    }

    protected Control CreateEnumEditor(System.Type type) => (Control) new EnumCellEditor(type);
  }
}
