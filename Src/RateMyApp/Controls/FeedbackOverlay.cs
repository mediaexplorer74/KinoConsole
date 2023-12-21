// Decompiled with JetBrains decompiler
// Type: RateMyApp.Controls.FeedbackOverlay


using RateMyApp.Helpers;
using RateMyApp.Resources;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace RateMyApp.Controls
{
  public class FeedbackOverlay : UserControl
  {
    public static readonly DependencyProperty EnableAnimationProperty = DependencyProperty.Register("EnableAnimation", typeof (bool), typeof (FeedbackOverlay), new PropertyMetadata((object) true, (PropertyChangedCallback) null));
    public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register("IsVisible", typeof (bool), typeof (FeedbackOverlay), new PropertyMetadata((object) false, (PropertyChangedCallback) null));
    public static readonly DependencyProperty IsNotVisibleProperty = DependencyProperty.Register("IsNotVisible", typeof (bool), typeof (FeedbackOverlay), new PropertyMetadata((object) true, (PropertyChangedCallback) null));
    public static readonly DependencyProperty RatingTitleProperty = DependencyProperty.Register("RatingTitle", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.RatingTitle, (PropertyChangedCallback) null));
    public static readonly DependencyProperty RatingMessage1Property = DependencyProperty.Register("RatingMessage1", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.RatingMessage1, (PropertyChangedCallback) null));
    public static readonly DependencyProperty RatingMessage2Property = DependencyProperty.Register("RatingMessage2", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.RatingMessage2, (PropertyChangedCallback) null));
    public static readonly DependencyProperty RatingYesProperty = DependencyProperty.Register("RatingYes", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.RatingYes, (PropertyChangedCallback) null));
    public static readonly DependencyProperty RatingNoProperty = DependencyProperty.Register("RatingNo", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.RatingNo, (PropertyChangedCallback) null));
    public static readonly DependencyProperty FeedbackTitleProperty = DependencyProperty.Register("FeedbackTitle", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.FeedbackTitle, (PropertyChangedCallback) null));
    public static readonly DependencyProperty FeedbackMessage1Property = DependencyProperty.Register("FeedbackMessage1", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.FeedbackMessage1, (PropertyChangedCallback) null));
    public static readonly DependencyProperty FeedbackYesProperty = DependencyProperty.Register("FeedbackYes", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.FeedbackYes, (PropertyChangedCallback) null));
    public static readonly DependencyProperty FeedbackNoProperty = DependencyProperty.Register("FeedbackNo", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.FeedbackNo, (PropertyChangedCallback) null));
    public static readonly DependencyProperty FeedbackToProperty = DependencyProperty.Register("FeedbackTo", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) null, (PropertyChangedCallback) null));
    public static readonly DependencyProperty FeedbackSubjectProperty = DependencyProperty.Register("FeedbackSubject", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.FeedbackSubject, (PropertyChangedCallback) null));
    public static readonly DependencyProperty FeedbackBodyProperty = DependencyProperty.Register("FeedbackBody", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) AppResources.FeedbackBody, (PropertyChangedCallback) null));
    public static readonly DependencyProperty CompanyNameProperty = DependencyProperty.Register("CompanyName", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) null, (PropertyChangedCallback) null));
    public static readonly DependencyProperty ApplicationNameProperty = DependencyProperty.Register("ApplicationName", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) null, (PropertyChangedCallback) null));
    public static readonly DependencyProperty FirstCountProperty = DependencyProperty.Register("FirstCount", typeof (int), typeof (FeedbackOverlay), new PropertyMetadata((object) 5, (PropertyChangedCallback) null));
    public static readonly DependencyProperty SecondCountProperty = DependencyProperty.Register("SecondCount", typeof (int), typeof (FeedbackOverlay), new PropertyMetadata((object) 10, (PropertyChangedCallback) null));
    public static readonly DependencyProperty CountDaysProperty = DependencyProperty.Register("CountDays", typeof (bool), typeof (FeedbackOverlay), new PropertyMetadata((object) false, (PropertyChangedCallback) null));
    public static readonly DependencyProperty LanguageOverrideProperty = DependencyProperty.Register("LanguageOverride", typeof (string), typeof (FeedbackOverlay), new PropertyMetadata((object) null, (PropertyChangedCallback) null));
    private PhoneApplicationFrame _rootFrame;
    internal Storyboard showContent;
    internal Storyboard hideContent;
    internal Grid LayoutRoot;
    internal Border content;
    internal TextBlock title;
    internal TextBlock message;
    internal Button yesButton;
    internal Button noButton;
    internal PlaneProjection xProjection;
    private bool _contentLoaded;

    public static void SetEnableAnimation(FeedbackOverlay element, bool value) => ((DependencyObject) element).SetValue(FeedbackOverlay.EnableAnimationProperty, (object) value);

    public static bool GetEnableAnimation(FeedbackOverlay element) => (bool) ((DependencyObject) element).GetValue(FeedbackOverlay.EnableAnimationProperty);

    public static void SetIsVisible(FeedbackOverlay element, bool value) => ((DependencyObject) element).SetValue(FeedbackOverlay.IsVisibleProperty, (object) value);

    public static bool GetIsVisible(FeedbackOverlay element) => (bool) ((DependencyObject) element).GetValue(FeedbackOverlay.IsVisibleProperty);

    public static void SetIsNotVisible(FeedbackOverlay element, bool value) => ((DependencyObject) element).SetValue(FeedbackOverlay.IsNotVisibleProperty, (object) value);

    public static bool GetIsNotVisible(FeedbackOverlay element) => (bool) ((DependencyObject) element).GetValue(FeedbackOverlay.IsNotVisibleProperty);

    public static void SetRatingTitle(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.RatingTitleProperty, (object) value);

    public static string GetRatingTitle(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.RatingTitleProperty);

    public static void SetRatingMessage1(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.RatingMessage1Property, (object) value);

    public static string GetRatingMessage1(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.RatingMessage1Property);

    public static void SetRatingMessage2(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.RatingMessage2Property, (object) value);

    public static string GetRatingMessage2(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.RatingMessage2Property);

    public static void SetRatingYes(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.RatingYesProperty, (object) value);

    public static string GetRatingYes(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.RatingYesProperty);

    public static void SetRatingNo(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.RatingNoProperty, (object) value);

    public static string GetRatingNo(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.RatingNoProperty);

    public static void SetFeedbackTitle(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.FeedbackTitleProperty, (object) value);

    public static string GetFeedbackTitle(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.FeedbackTitleProperty);

    public static void SetFeedbackMessage1(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.FeedbackMessage1Property, (object) value);

    public static string GetFeedbackMessage1(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.FeedbackMessage1Property);

    public static void SetFeedbackYes(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.FeedbackYesProperty, (object) value);

    public static string GetFeedbackYes(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.FeedbackYesProperty);

    public static void SetFeedbackNo(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.FeedbackNoProperty, (object) value);

    public static string GetFeedbackNo(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.FeedbackNoProperty);

    public static void SetFeedbackTo(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.FeedbackToProperty, (object) value);

    public static string GetFeedbackTo(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.FeedbackToProperty);

    public static void SetFeedbackSubject(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.FeedbackSubjectProperty, (object) value);

    public static string GetFeedbackSubject(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.FeedbackSubjectProperty);

    public static void SetFeedbackBody(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.FeedbackBodyProperty, (object) value);

    public static string GetFeedbackBody(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.FeedbackBodyProperty);

    public static void SetCompanyName(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.CompanyNameProperty, (object) value);

    public static string GetCompanyName(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.CompanyNameProperty);

    public static void SetApplicationName(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.ApplicationNameProperty, (object) value);

    public static string GetApplicationName(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.ApplicationNameProperty);

    public static void SetFirstCount(FeedbackOverlay element, int value) => ((DependencyObject) element).SetValue(FeedbackOverlay.FirstCountProperty, (object) value);

    public static int GetFirstCount(FeedbackOverlay element) => (int) ((DependencyObject) element).GetValue(FeedbackOverlay.FirstCountProperty);

    public static void SetSecondCount(FeedbackOverlay element, int value) => ((DependencyObject) element).SetValue(FeedbackOverlay.SecondCountProperty, (object) value);

    public static int GetSecondCount(FeedbackOverlay element) => (int) ((DependencyObject) element).GetValue(FeedbackOverlay.SecondCountProperty);

    public static void SetCountDays(FeedbackOverlay element, bool value) => ((DependencyObject) element).SetValue(FeedbackOverlay.CountDaysProperty, (object) value);

    public static bool GetCountDays(FeedbackOverlay element) => (bool) ((DependencyObject) element).GetValue(FeedbackOverlay.CountDaysProperty);

    public static void SetLanguageOverride(FeedbackOverlay element, string value) => ((DependencyObject) element).SetValue(FeedbackOverlay.LanguageOverrideProperty, (object) value);

    public static string GetLanguageOverride(FeedbackOverlay element) => (string) ((DependencyObject) element).GetValue(FeedbackOverlay.LanguageOverrideProperty);

    public event EventHandler VisibilityChanged;

    public string Title
    {
      set
      {
        if (!(this.title.Text != value))
          return;
        this.title.Text = value;
      }
    }

    public string Message
    {
      set
      {
        if (!(this.message.Text != value))
          return;
        this.message.Text = value;
      }
    }

    public string NoText
    {
      set
      {
        if (!((string) ((ContentControl) this.noButton).Content != value))
          return;
        ((ContentControl) this.noButton).Content = (object) value;
      }
    }

    public string YesText
    {
      set
      {
        if (!((string) ((ContentControl) this.yesButton).Content != value))
          return;
        ((ContentControl) this.yesButton).Content = (object) value;
      }
    }

    public FeedbackOverlay()
    {
      this.InitializeComponent();
      ((ButtonBase) this.yesButton).Click += new RoutedEventHandler(this.yesButton_Click);
      ((ButtonBase) this.noButton).Click += new RoutedEventHandler(this.noButton_Click);
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.FeedbackOverlay_Loaded);
      ((Timeline) this.hideContent).Completed += new EventHandler(this.hideContent_Completed);
    }

    public void Reset() => FeedbackHelper.Default.Reset();

    private void FeedbackOverlay_Loaded(object sender, RoutedEventArgs e)
    {
      if (FeedbackOverlay.GetFeedbackTo(this) == null || FeedbackOverlay.GetFeedbackTo(this).Length <= 0)
        throw new ArgumentNullException("FeedbackTo", "Mandatory property not defined in FeedbackOverlay.");
      if (FeedbackOverlay.GetApplicationName(this) == null || FeedbackOverlay.GetApplicationName(this).Length <= 0)
        throw new ArgumentNullException("ApplicationName", "Mandatory property not defined in FeedbackOverlay.");
      if (FeedbackOverlay.GetCompanyName(this) == null || FeedbackOverlay.GetCompanyName(this).Length <= 0)
        throw new ArgumentNullException("CompanyName", "Mandatory property not defined in FeedbackOverlay.");
      if (FeedbackOverlay.GetLanguageOverride(this) != null)
        this.OverrideLanguage();
      FeedbackHelper.Default.FirstCount = FeedbackOverlay.GetFirstCount(this);
      FeedbackHelper.Default.SecondCount = FeedbackOverlay.GetSecondCount(this);
      FeedbackHelper.Default.CountDays = FeedbackOverlay.GetCountDays(this);
      FeedbackHelper.Default.Launching();
      this.AttachBackKeyPressed();
      if (FeedbackOverlay.GetEnableAnimation(this))
      {
        ((UIElement) this.LayoutRoot).Opacity = 0.0;
        this.xProjection.RotationX = 90.0;
      }
      if (FeedbackHelper.Default.State == FeedbackState.FirstReview)
      {
        this.SetVisibility(true);
        this.SetupFirstMessage();
        if (!FeedbackOverlay.GetEnableAnimation(this))
          return;
        this.showContent.Begin();
      }
      else if (FeedbackHelper.Default.State == FeedbackState.SecondReview)
      {
        this.SetVisibility(true);
        this.SetupSecondMessage();
        if (!FeedbackOverlay.GetEnableAnimation(this))
          return;
        this.showContent.Begin();
      }
      else
      {
        this.SetVisibility(false);
        FeedbackHelper.Default.State = FeedbackState.Inactive;
      }
    }

    private void AttachBackKeyPressed()
    {
      if (this._rootFrame != null)
        return;
      this._rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
      this._rootFrame.BackKeyPress += new EventHandler<CancelEventArgs>(this.FeedbackOverlay_BackKeyPress);
    }

    private void FeedbackOverlay_BackKeyPress(object sender, CancelEventArgs e)
    {
      if (((UIElement) this).Visibility != null)
        return;
      this.OnNoClick();
      e.Cancel = true;
    }

    private void SetupFirstMessage()
    {
      this.Title = string.Format(FeedbackOverlay.GetRatingTitle(this), (object) this.GetApplicationName());
      this.Message = FeedbackOverlay.GetRatingMessage1(this);
      this.YesText = FeedbackOverlay.GetRatingYes(this);
      this.NoText = FeedbackOverlay.GetRatingNo(this);
    }

    private void SetupSecondMessage()
    {
      this.Title = string.Format(FeedbackOverlay.GetRatingTitle(this), (object) this.GetApplicationName());
      this.Message = FeedbackOverlay.GetRatingMessage2(this);
      this.YesText = FeedbackOverlay.GetRatingYes(this);
      this.NoText = FeedbackOverlay.GetRatingNo(this);
    }

    private void SetupFeedbackMessage()
    {
      this.Title = FeedbackOverlay.GetFeedbackTitle(this);
      this.Message = string.Format(FeedbackOverlay.GetFeedbackMessage1(this), (object) this.GetApplicationName());
      this.YesText = FeedbackOverlay.GetFeedbackYes(this);
      this.NoText = FeedbackOverlay.GetFeedbackNo(this);
    }

    private void noButton_Click(object sender, RoutedEventArgs e) => this.OnNoClick();

    private void OnNoClick()
    {
      if (FeedbackOverlay.GetEnableAnimation(this))
        this.hideContent.Begin();
      else
        this.ShowFeedback();
    }

    private void hideContent_Completed(object sender, EventArgs e) => this.ShowFeedback();

    private void ShowFeedback()
    {
      if (FeedbackHelper.Default.State == FeedbackState.FirstReview)
      {
        this.SetupFeedbackMessage();
        FeedbackHelper.Default.State = FeedbackState.Feedback;
        if (!FeedbackOverlay.GetEnableAnimation(this))
          return;
        this.showContent.Begin();
      }
      else
      {
        this.SetVisibility(false);
        FeedbackHelper.Default.State = FeedbackState.Inactive;
      }
    }

    private void yesButton_Click(object sender, RoutedEventArgs e)
    {
      this.SetVisibility(false);
      if (FeedbackHelper.Default.State == FeedbackState.FirstReview)
        this.Review();
      else if (FeedbackHelper.Default.State == FeedbackState.SecondReview)
        this.Review();
      else if (FeedbackHelper.Default.State == FeedbackState.Feedback)
        this.Feedback();
      FeedbackHelper.Default.State = FeedbackState.Inactive;
    }

    private void Review()
    {
      FeedbackHelper.Default.Reviewed();
      new MarketplaceReviewTask().Show();
    }

    private void Feedback()
    {
      string str1 = Assembly.GetExecutingAssembly().FullName.Split(',')[1].Split('=')[1];
      string str2 = FeedbackOverlay.GetCompanyName(this);
      if (str2 == null || str2.Length <= 0)
        str2 = "<Company>";
      string str3 = string.Format(FeedbackOverlay.GetFeedbackBody(this), (object) DeviceStatus.DeviceName, (object) DeviceStatus.DeviceManufacturer, (object) DeviceStatus.DeviceFirmwareVersion, (object) DeviceStatus.DeviceHardwareVersion, (object) str1, (object) str2);
      new EmailComposeTask()
      {
        To = FeedbackOverlay.GetFeedbackTo(this),
        Subject = string.Format(FeedbackOverlay.GetFeedbackSubject(this), (object) this.GetApplicationName()),
        Body = str3
      }.Show();
    }

    private void SetVisibility(bool visible)
    {
      bool isVisible = FeedbackOverlay.GetIsVisible(this);
      if (visible)
      {
        this.PreparePanoramaPivot(false);
        FeedbackOverlay.SetIsVisible(this, true);
        FeedbackOverlay.SetIsNotVisible(this, false);
        ((UIElement) this).Visibility = (Visibility) 0;
      }
      else
      {
        this.PreparePanoramaPivot(true);
        FeedbackOverlay.SetIsVisible(this, false);
        FeedbackOverlay.SetIsNotVisible(this, true);
        ((UIElement) this).Visibility = (Visibility) 1;
      }
      if (isVisible == visible)
        return;
      this.OnVisibilityChanged();
    }

    private void OnVisibilityChanged()
    {
      if (this.VisibilityChanged == null)
        return;
      this.VisibilityChanged((object) this, new EventArgs());
    }

    private void PreparePanoramaPivot(bool hitTestVisible)
    {
      DependencyObject parent = VisualTreeHelper.GetParent((DependencyObject) this);
      int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
      for (int index = 0; index < childrenCount; ++index)
      {
        DependencyObject child = VisualTreeHelper.GetChild(parent, index);
        Type type = child.GetType();
        if (type.FullName == "Microsoft.Phone.Controls.Panorama" || type.FullName == "Microsoft.Phone.Controls.Pivot")
          ((UIElement) child).IsHitTestVisible = hitTestVisible;
      }
    }

    private void OverrideLanguage()
    {
      CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
      CultureInfo cultureInfo = new CultureInfo(FeedbackOverlay.GetLanguageOverride(this));
      Thread.CurrentThread.CurrentCulture = cultureInfo;
      Thread.CurrentThread.CurrentUICulture = cultureInfo;
      FeedbackOverlay.SetFeedbackBody(this, AppResources.FeedbackBody);
      FeedbackOverlay.SetFeedbackMessage1(this, string.Format(AppResources.FeedbackMessage1, (object) this.GetApplicationName()));
      FeedbackOverlay.SetFeedbackNo(this, AppResources.FeedbackNo);
      FeedbackOverlay.SetFeedbackSubject(this, string.Format(AppResources.FeedbackSubject, (object) this.GetApplicationName()));
      FeedbackOverlay.SetFeedbackTitle(this, AppResources.FeedbackTitle);
      FeedbackOverlay.SetFeedbackYes(this, AppResources.FeedbackYes);
      FeedbackOverlay.SetRatingMessage1(this, AppResources.RatingMessage1);
      FeedbackOverlay.SetRatingMessage2(this, AppResources.RatingMessage2);
      FeedbackOverlay.SetRatingNo(this, AppResources.RatingNo);
      FeedbackOverlay.SetRatingTitle(this, string.Format(AppResources.RatingTitle, (object) this.GetApplicationName()));
      FeedbackOverlay.SetRatingYes(this, AppResources.RatingYes);
      Thread.CurrentThread.CurrentCulture = currentUiCulture;
      Thread.CurrentThread.CurrentUICulture = currentUiCulture;
    }

    private string GetApplicationName()
    {
      string applicationName = FeedbackOverlay.GetApplicationName(this);
      if (applicationName == null || applicationName.Length <= 0)
      {
        applicationName = Application.Current.ToString();
        if (applicationName.EndsWith(".App"))
          applicationName = applicationName.Remove(applicationName.LastIndexOf(".App"));
      }
      return applicationName;
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/RateMyApp;component/Controls/FeedbackOverlay.xaml", UriKind.Relative));
      this.showContent = (Storyboard) ((FrameworkElement) this).FindName("showContent");
      this.hideContent = (Storyboard) ((FrameworkElement) this).FindName("hideContent");
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.content = (Border) ((FrameworkElement) this).FindName("content");
      this.title = (TextBlock) ((FrameworkElement) this).FindName("title");
      this.message = (TextBlock) ((FrameworkElement) this).FindName("message");
      this.yesButton = (Button) ((FrameworkElement) this).FindName("yesButton");
      this.noButton = (Button) ((FrameworkElement) this).FindName("noButton");
      this.xProjection = (PlaneProjection) ((FrameworkElement) this).FindName("xProjection");
    }
  }
}
