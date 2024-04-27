// Decompiled with JetBrains decompiler
// Type: TAFactory.IconPack.ResourceName
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Runtime.InteropServices;


namespace TAFactory.IconPack
{
  public class ResourceName : IDisposable
  {
    private int? _id;
    private string _name;
    private IntPtr _value;

    public int? Id
    {
      get => this._id;
      private set => this._id = value;
    }

    public string Name
    {
      get => this._name;
      private set => this._name = value;
    }

    public IntPtr Value
    {
      get
      {
        if (this.IsIntResource)
          return new IntPtr(this.Id.Value);
        if (this._value == IntPtr.Zero)
          this._value = Marshal.StringToHGlobalAuto(this.Name);
        return this._value;
      }
      private set => this._value = value;
    }

    public bool IsIntResource => this.Id.HasValue;

    public ResourceName(IntPtr lpName)
    {
      if ((int) lpName >>> 16 == 0)
      {
        this.Id = new int?(lpName.ToInt32());
        this.Name = (string) null;
      }
      else
      {
        this.Id = new int?();
        this.Name = Marshal.PtrToStringAuto(lpName);
      }
    }

    ~ResourceName() => this.Dispose();

    public override string ToString() => this.IsIntResource ? "#" + this.Id.ToString() : this.Name;

    public void Free()
    {
      if (!(this._value != IntPtr.Zero))
        return;
      try
      {
        Marshal.FreeHGlobal(this._value);
      }
      catch
      {
      }
      this._value = IntPtr.Zero;
    }

    public void Dispose() => this.Free();
  }
}
