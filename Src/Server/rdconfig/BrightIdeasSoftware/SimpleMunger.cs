// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.SimpleMunger
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Reflection;


namespace BrightIdeasSoftware
{
  public class SimpleMunger
  {
    private readonly string aspectName;
    private Type cachedTargetType;
    private string cachedName;
    private int cachedNumberParameters;
    private FieldInfo resolvedFieldInfo;
    private PropertyInfo resolvedPropertyInfo;
    private MethodInfo resolvedMethodInfo;
    private PropertyInfo indexerPropertyInfo;

    public SimpleMunger(string aspectName) => this.aspectName = aspectName;

    public string AspectName => this.aspectName;

    public object GetValue(object target)
    {
      if (target == null)
        return (object) null;
      this.ResolveName(target, this.AspectName, 0);
      try
      {
        if ((object) this.resolvedPropertyInfo != null)
          return this.resolvedPropertyInfo.GetValue(target, (object[]) null);
        if ((object) this.resolvedMethodInfo != null)
          return this.resolvedMethodInfo.Invoke(target, (object[]) null);
        if ((object) this.resolvedFieldInfo != null)
          return this.resolvedFieldInfo.GetValue(target);
        if ((object) this.indexerPropertyInfo != null)
          return this.indexerPropertyInfo.GetValue(target, new object[1]
          {
            (object) this.AspectName
          });
      }
      catch (Exception ex)
      {
        throw new MungerException(this, target, ex);
      }
      throw new MungerException(this, target, (Exception) new MissingMethodException());
    }

    public bool PutValue(object target, object value)
    {
      if (target == null)
        return false;
      this.ResolveName(target, this.AspectName, 1);
      try
      {
        if ((object) this.resolvedPropertyInfo != null)
        {
          this.resolvedPropertyInfo.SetValue(target, value, (object[]) null);
          return true;
        }
        if ((object) this.resolvedMethodInfo != null)
        {
          this.resolvedMethodInfo.Invoke(target, new object[1]
          {
            value
          });
          return true;
        }
        if ((object) this.resolvedFieldInfo != null)
        {
          this.resolvedFieldInfo.SetValue(target, value);
          return true;
        }
        if ((object) this.indexerPropertyInfo != null)
        {
          this.indexerPropertyInfo.SetValue(target, value, new object[1]
          {
            (object) this.AspectName
          });
          return true;
        }
      }
      catch (Exception ex)
      {
        throw new MungerException(this, target, ex);
      }
      return false;
    }

    private void ResolveName(object target, string name, int numberMethodParameters)
    {
      if ((object) this.cachedTargetType == (object) target.GetType() && this.cachedName == name && this.cachedNumberParameters == numberMethodParameters)
        return;
      this.cachedTargetType = target.GetType();
      this.cachedName = name;
      this.cachedNumberParameters = numberMethodParameters;
      this.resolvedFieldInfo = (FieldInfo) null;
      this.resolvedPropertyInfo = (PropertyInfo) null;
      this.resolvedMethodInfo = (MethodInfo) null;
      this.indexerPropertyInfo = (PropertyInfo) null;
      foreach (PropertyInfo property in target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
      {
        if (property.Name == name)
        {
          this.resolvedPropertyInfo = property;
          return;
        }
        if ((object) this.indexerPropertyInfo == null && property.Name == "Item")
        {
          ParameterInfo[] parameters = property.GetGetMethod().GetParameters();
          if (parameters.Length > 0)
          {
            Type parameterType = parameters[0].ParameterType;
            if ((object) parameterType == (object) typeof (string) || (object) parameterType == (object) typeof (object))
              this.indexerPropertyInfo = property;
          }
        }
      }
      foreach (FieldInfo field in target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
      {
        if (field.Name == name)
        {
          this.resolvedFieldInfo = field;
          return;
        }
      }
      foreach (MethodInfo method in target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
      {
        if (method.Name == name && method.GetParameters().Length == numberMethodParameters)
        {
          this.resolvedMethodInfo = method;
          break;
        }
      }
    }
  }
}
