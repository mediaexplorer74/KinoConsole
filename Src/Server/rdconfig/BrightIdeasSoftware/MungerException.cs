// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.MungerException
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace BrightIdeasSoftware
{
  public class MungerException : ApplicationException
  {
    private readonly SimpleMunger munger;
    private readonly object target;

    public MungerException(SimpleMunger munger, object target, Exception ex)
      : base("Munger failed", ex)
    {
      this.munger = munger;
      this.target = target;
    }

    public SimpleMunger Munger => this.munger;

    public object Target => this.target;
  }
}
