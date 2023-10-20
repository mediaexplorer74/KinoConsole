// Decompiled with JetBrains decompiler
// Type: RateMyApp.Helpers.FeedbackHelper
// Assembly: RateMyApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24764F99-C481-4414-9E6E-223BAD44A77B
// Assembly location: C:\Users\Admin\Desktop\re\KC\RateMyApp.dll

using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.ComponentModel;

namespace RateMyApp.Helpers
{
  public class FeedbackHelper : INotifyPropertyChanged
  {
    private const string LaunchCountKey = "RATE_MY_APP_LAUNCH_COUNT";
    private const string ReviewedKey = "RATE_MY_APP_REVIEWED";
    private const string LastLaunchDateKey = "RATE_MY_APP_LAST_LAUNCH_DATE";
    private int firstCount;
    private int secondCount;
    private FeedbackState state;
    private int launchCount;
    public static readonly FeedbackHelper Default = new FeedbackHelper();
    private bool reviewed;
    private DateTime lastLaunchDate = new DateTime();

    public event PropertyChangedEventHandler PropertyChanged;

    public DateTime LastLaunchDate
    {
      get => this.lastLaunchDate;
      internal set
      {
        this.lastLaunchDate = value;
        this.OnPropertyChanged(nameof (LastLaunchDate));
      }
    }

    public bool IsReviewed
    {
      get => this.reviewed;
      internal set
      {
        this.reviewed = value;
        this.OnPropertyChanged(nameof (IsReviewed));
      }
    }

    public FeedbackState State
    {
      get => this.state;
      internal set
      {
        this.state = value;
        this.OnPropertyChanged(nameof (State));
      }
    }

    public int LaunchCount
    {
      get => this.launchCount;
      internal set
      {
        this.launchCount = value;
        this.OnPropertyChanged(nameof (LaunchCount));
      }
    }

    public int FirstCount
    {
      get => this.firstCount;
      internal set
      {
        this.firstCount = value;
        this.OnPropertyChanged(nameof (FirstCount));
      }
    }

    public int SecondCount
    {
      get => this.secondCount;
      internal set
      {
        this.secondCount = value;
        this.OnPropertyChanged(nameof (SecondCount));
      }
    }

    public bool CountDays { get; set; }

    private FeedbackHelper() => this.State = FeedbackState.Active;

    public void Launching()
    {
      if (new LicenseInformation().IsTrial() || PhoneApplicationService.Current.StartupMode != 1 || this.State != FeedbackState.Active)
        return;
      this.LoadState();
    }

    public void Reviewed()
    {
      this.IsReviewed = true;
      this.StoreState();
    }

    public void Reset()
    {
      this.LaunchCount = 0;
      this.IsReviewed = false;
      this.LastLaunchDate = DateTime.Now;
      this.StoreState();
    }

    private void LoadState()
    {
      try
      {
        this.LaunchCount = StorageHelper.GetSetting<int>("RATE_MY_APP_LAUNCH_COUNT");
        this.IsReviewed = StorageHelper.GetSetting<bool>("RATE_MY_APP_REVIEWED");
        this.LastLaunchDate = StorageHelper.GetSetting<DateTime>("RATE_MY_APP_LAST_LAUNCH_DATE");
        if (this.reviewed)
          return;
        if (!this.CountDays || this.lastLaunchDate.Date < DateTime.Now.Date)
        {
          ++this.LaunchCount;
          this.LastLaunchDate = DateTime.Now;
        }
        if (this.LaunchCount == this.FirstCount)
          this.State = FeedbackState.FirstReview;
        else if (this.LaunchCount == this.SecondCount)
          this.State = FeedbackState.SecondReview;
        this.StoreState();
      }
      catch (Exception ex)
      {
      }
    }

    private void StoreState()
    {
      try
      {
        StorageHelper.StoreSetting("RATE_MY_APP_LAUNCH_COUNT", (object) this.LaunchCount, true);
        StorageHelper.StoreSetting("RATE_MY_APP_REVIEWED", (object) this.reviewed, true);
        StorageHelper.StoreSetting("RATE_MY_APP_LAST_LAUNCH_DATE", (object) this.lastLaunchDate, true);
      }
      catch (Exception ex)
      {
      }
    }

    protected void OnPropertyChanged(string name)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(name));
    }

    public void Review()
    {
      this.Reviewed();
      new MarketplaceReviewTask().Show();
    }
  }
}
