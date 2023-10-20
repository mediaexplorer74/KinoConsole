// Decompiled with JetBrains decompiler
// Type: FlurryWP8SDK.Models.Parameter
// Assembly: FlurryWP8SDK, Version=3.1.1.0, Culture=neutral, PublicKeyToken=d822942b4ec01210
// MVID: 88AADB87-C553-4277-9412-5C940D56AE1D
// Assembly location: C:\Users\Admin\Desktop\re\KC\FlurryWP8SDK.dll

using System;

namespace FlurryWP8SDK.Models
{
  public class Parameter
  {
    private const int c8a86469337b4cd606a787ad80716a69b = 255;
    private string ca43a3faafde581bd2b7a0af0943f0cb3;
    private string cc395d0102e4b4ebffed852afbf6520b0;

    internal Parameter()
    {
    }

    public Parameter(string name, string value)
    {
      this.Name = name;
      this.Value = value;
    }

    public string Name
    {
      get => this.ca43a3faafde581bd2b7a0af0943f0cb3;
      set
      {
        string str;
        if (value == null)
        {
                    //RnD
          //RuntimeMethodHandle runtimeMethodHandle1 = __methodref (Parameter.set_Name);
        }
        else if (value.Length > (int) byte.MaxValue)
        {
                    //RnD
          //RuntimeMethodHandle runtimeMethodHandle2 = __methodref (Parameter.set_Name);
          str = value.Substring(0, (int) byte.MaxValue);
          goto label_5;
        }
        str = value;
label_5:
        this.ca43a3faafde581bd2b7a0af0943f0cb3 = str;
      }
    }

    public string Value
        {
            get => this.cc395d0102e4b4ebffed852afbf6520b0;
            set
            {
                this.cc395d0102e4b4ebffed852afbf6520b0 = value == null
                    || value.Length <= (int)byte.MaxValue 
                    ? value : value.Substring(0, (int)byte.MaxValue);
            }
        }
    }
}
