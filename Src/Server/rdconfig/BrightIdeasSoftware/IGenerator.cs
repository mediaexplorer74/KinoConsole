﻿// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IGenerator
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Collections.Generic;


namespace BrightIdeasSoftware
{
  public interface IGenerator
  {
    void GenerateAndReplaceColumns(ObjectListView olv, Type type, bool allProperties);

    IList<OLVColumn> GenerateColumns(Type type, bool allProperties);
  }
}
