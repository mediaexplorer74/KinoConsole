// Decompiled with JetBrains decompiler
// Type: KinoConsole.Resources.AppResources
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace KinoConsole.Resources
{

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
        if (object.ReferenceEquals((object)AppResources.resourceMan, (object)null))
        {
            AppResources.resourceMan = new ResourceManager("KinoConsole.Resources.AppResources",
                typeof(AppResources).Assembly);
        }
        return AppResources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
      get => AppResources.resourceCulture;
      set => AppResources.resourceCulture = value;
    }

        public static string ResourceFlowDirection
        {
            get
            {
                return AppResources.ResourceManager.GetString(nameof(ResourceFlowDirection), 
                    AppResources.resourceCulture);
            }
        }

        public static string ResourceLanguage
        {
            get
            {
                return AppResources.ResourceManager.GetString(nameof(ResourceLanguage), 
                    AppResources.resourceCulture);
            }
        }

        public static string ApplicationTitle
        {
            get
            {
                return AppResources.ResourceManager.GetString(nameof(ApplicationTitle), 
                    AppResources.resourceCulture);
            }
        }

        public static string AppBarButtonText
        {
            get
            {
                return AppResources.ResourceManager.GetString(nameof(AppBarButtonText), 
                    AppResources.resourceCulture);
            }
        }

        public static string AppBarMenuItemText
        {
            get
            {
                return AppResources.ResourceManager.GetString(nameof(AppBarMenuItemText), 
                    AppResources.resourceCulture);
            }
        }
    }
}
