// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.FlagBitSetFilter
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections;
using System.Collections.Generic;


namespace BrightIdeasSoftware
{
  public class FlagBitSetFilter : OneOfFilter
  {
    private List<ulong> possibleValuesAsUlongs = new List<ulong>();

    public FlagBitSetFilter(AspectGetterDelegate valueGetter, ICollection possibleValues)
      : base(valueGetter, possibleValues)
    {
      this.ConvertPossibleValues();
    }

    public override IList PossibleValues
    {
      get => base.PossibleValues;
      set
      {
        base.PossibleValues = value;
        this.ConvertPossibleValues();
      }
    }

    private void ConvertPossibleValues()
    {
      this.possibleValuesAsUlongs = new List<ulong>();
      foreach (object possibleValue in (IEnumerable) this.PossibleValues)
        this.possibleValuesAsUlongs.Add(Convert.ToUInt64(possibleValue));
    }

    protected override bool DoesValueMatch(object result)
    {
      try
      {
        ulong uint64 = Convert.ToUInt64(result);
        foreach (ulong possibleValuesAsUlong in this.possibleValuesAsUlongs)
        {
          if (((long) uint64 & (long) possibleValuesAsUlong) == (long) possibleValuesAsUlong)
            return true;
        }
        return false;
      }
      catch (InvalidCastException ex)
      {
        return false;
      }
      catch (FormatException ex)
      {
        return false;
      }
    }
  }
}
