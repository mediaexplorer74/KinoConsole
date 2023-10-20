// Decompiled with JetBrains decompiler
// Type: RateMyApp.Resources.AppResources
// Assembly: RateMyApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24764F99-C481-4414-9E6E-223BAD44A77B
// Assembly location: C:\Users\Admin\Desktop\re\KC\RateMyApp.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace RateMyApp.Resources
{
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
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
          AppResources.resourceMan = new ResourceManager("RateMyApp.Resources.AppResources", typeof (AppResources).Assembly);
        return AppResources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
      get => AppResources.resourceCulture;
      set => AppResources.resourceCulture = value;
    }

    public static string FeedbackBody => AppResources.ResourceManager.GetString(nameof (FeedbackBody), AppResources.resourceCulture);

    public static string FeedbackMessage1 => AppResources.ResourceManager.GetString(nameof (FeedbackMessage1), AppResources.resourceCulture);

    public static string FeedbackNo => AppResources.ResourceManager.GetString(nameof (FeedbackNo), AppResources.resourceCulture);

    public static string FeedbackSubject => AppResources.ResourceManager.GetString(nameof (FeedbackSubject), AppResources.resourceCulture);

    public static string FeedbackTitle => AppResources.ResourceManager.GetString(nameof (FeedbackTitle), AppResources.resourceCulture);

    public static string FeedbackYes => AppResources.ResourceManager.GetString(nameof (FeedbackYes), AppResources.resourceCulture);

    public static string RatingMessage1 => AppResources.ResourceManager.GetString(nameof (RatingMessage1), AppResources.resourceCulture);

    public static string RatingMessage2 => AppResources.ResourceManager.GetString(nameof (RatingMessage2), AppResources.resourceCulture);

    public static string RatingNo => AppResources.ResourceManager.GetString(nameof (RatingNo), AppResources.resourceCulture);

    public static string RatingTitle => AppResources.ResourceManager.GetString(nameof (RatingTitle), AppResources.resourceCulture);

    public static string RatingYes => AppResources.ResourceManager.GetString(nameof (RatingYes), AppResources.resourceCulture);

    public static string ResourceFlowDirection => AppResources.ResourceManager.GetString(nameof (ResourceFlowDirection), AppResources.resourceCulture);

    public static string ResourceLanguage => AppResources.ResourceManager.GetString(nameof (ResourceLanguage), AppResources.resourceCulture);
  }
}
