﻿// Decompiled with JetBrains decompiler
// Type: KinoConsole.Resources.AppResources
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace KinoConsole.Resources
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  public class AppResources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal AppResources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) AppResources.resourceMan, (object) null))
          AppResources.resourceMan = new ResourceManager("KinoConsole.Resources.AppResources", typeof (AppResources).Assembly);
        return AppResources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
      get => AppResources.resourceCulture;
      set => AppResources.resourceCulture = value;
    }

    public static string ResourceFlowDirection => AppResources.ResourceManager.GetString(nameof (ResourceFlowDirection), AppResources.resourceCulture);

    public static string ResourceLanguage => AppResources.ResourceManager.GetString(nameof (ResourceLanguage), AppResources.resourceCulture);

    public static string ApplicationTitle => AppResources.ResourceManager.GetString(nameof (ApplicationTitle), AppResources.resourceCulture);

    public static string AppBarButtonText => AppResources.ResourceManager.GetString(nameof (AppBarButtonText), AppResources.resourceCulture);

    public static string AppBarMenuItemText => AppResources.ResourceManager.GetString(nameof (AppBarMenuItemText), AppResources.resourceCulture);
  }
}
