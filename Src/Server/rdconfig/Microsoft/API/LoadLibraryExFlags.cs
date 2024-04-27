// Decompiled with JetBrains decompiler
// Type: Microsoft.API.LoadLibraryExFlags
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;


namespace Microsoft.API
{
  [Flags]
  public enum LoadLibraryExFlags
  {
    DONT_RESOLVE_DLL_REFERENCES = 1,
    LOAD_LIBRARY_AS_DATAFILE = 2,
    LOAD_WITH_ALTERED_SEARCH_PATH = 8,
  }
}
