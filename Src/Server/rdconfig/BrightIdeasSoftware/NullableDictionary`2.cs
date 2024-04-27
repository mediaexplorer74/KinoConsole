// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.NullableDictionary`2
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;
using System.Collections.Generic;


namespace BrightIdeasSoftware
{
  internal class NullableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
  {
    private bool hasNullKey;
    private TValue nullValue;

    public new TValue this[TKey key]
    {
      get
      {
        if ((object) key != null)
          return base[key];
        if (this.hasNullKey)
          return this.nullValue;
        throw new KeyNotFoundException();
      }
      set
      {
        if ((object) key == null)
        {
          this.hasNullKey = true;
          this.nullValue = value;
        }
        else
          base[key] = value;
      }
    }

    public new bool ContainsKey(TKey key)
    {
      return (object) key != null ? base.ContainsKey(key) : this.hasNullKey;
    }

    public IList Keys
    {
      get
      {
        ArrayList keys = new ArrayList((ICollection) base.Keys);
        if (this.hasNullKey)
          keys.Add((object) null);
        return (IList) keys;
      }
    }

    public IList<TValue> Values
    {
      get
      {
        List<TValue> values = new List<TValue>((IEnumerable<TValue>) base.Values);
        if (this.hasNullKey)
          values.Add(this.nullValue);
        return (IList<TValue>) values;
      }
    }
  }
}
