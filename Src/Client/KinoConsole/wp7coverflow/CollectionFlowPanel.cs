// Decompiled with JetBrains decompiler
// Type: wp7coverflow.CollectionFlowPanel
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System;
using System.ComponentModel;
using System.Windows;
using Windows.Foundation;

//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace wp7coverflow
{
  public class CollectionFlowPanel : Panel
  {
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty AnimationFunctionProperty = DependencyProperty.Register(nameof (AnimationFunction), typeof (IEasingFunction), typeof (CollectionFlowPanel), (PropertyMetadata) null);
    public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(nameof (AnimationDuration), typeof (TimeSpan), typeof (CollectionFlowPanel), (PropertyMetadata) null);
    public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(nameof (ItemWidth), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(nameof (ItemHeight), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty ItemRelativeSizeProperty = DependencyProperty.Register(nameof (ItemRelativeSize), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty UseRelativeSizeProperty = DependencyProperty.Register(nameof (UseRelativeSize), typeof (bool), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty ItemVisibilityProperty = DependencyProperty.Register(nameof (ItemVisibility), typeof (int), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty ItemFadingProperty = DependencyProperty.Register(nameof (ItemFading), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty FocusedItemDistanceProperty = DependencyProperty.Register(nameof (FocusedItemDistance), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty UnfocusedItemDistanceProperty = DependencyProperty.Register(nameof (UnfocusedItemDistance), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty FocusedItemOffsetProperty = DependencyProperty.Register(nameof (FocusedItemOffset), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty UnfocusedItemOffsetProperty = DependencyProperty.Register(nameof (UnfocusedItemOffset), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty FocusedItemAngleProperty = DependencyProperty.Register(nameof (FocusedItemAngle), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty UnfocusedItemAngleProperty = DependencyProperty.Register(nameof (UnfocusedItemAngle), typeof (double), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));
    public static readonly DependencyProperty FocusedItemIndexProperty = DependencyProperty.Register(nameof (FocusedItemIndex), typeof (int), typeof (CollectionFlowPanel), new PropertyMetadata(new PropertyChangedCallback(CollectionFlowPanel.PropertyChanged)));

    public CollectionFlowPanel()
    {
      this.Orientation = (Orientation) 1;
      CubicEase cubicEase = new CubicEase();
      ((EasingFunctionBase) cubicEase).EasingMode = (EasingMode) 0;
      this.AnimationFunction = default;// (IEasingFunction)cubicEase; 
      this.AnimationDuration = TimeSpan.FromSeconds(0.4);
      this.ItemWidth = 100.0;
      this.ItemHeight = 100.0;
      this.ItemRelativeSize = 0.8;
      this.UseRelativeSize = false;
      this.ItemFading = 0.2;
      this.ItemVisibility = 3;
      this.FocusedItemDistance = 0.0;
      this.UnfocusedItemDistance = 200.0;
      this.FocusedItemOffset = 50.0;
      this.UnfocusedItemOffset = 10.0;
      this.FocusedItemAngle = 0.0;
      this.UnfocusedItemAngle = 45.0;
      this.FocusedItemIndex = -1;
    }

    private static void PropertyChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      ((UIElement) sender).InvalidateArrange();
    }

    protected virtual Size MeasureOverride(Size availableSize)
    {
      //(PresentationFrameworkCollection<UIElement>)
      foreach (FrameworkElement child in  this.Children)
      {
        if ((object) ((UIElement) child).RenderTransform.GetType() != (object) typeof (TranslateTransform))
          ((UIElement) child).RenderTransform = (Transform) new TranslateTransform();
        if (((UIElement) child).Projection == null)
          ((UIElement) child).Projection = (Projection) new PlaneProjection();
        ((UIElement) child).Measure(availableSize);
      }
      return availableSize;//((FrameworkElement) this).MeasureOverride(availableSize);
    }

    protected virtual Size ArrangeOverride(Size finalSize)
    {
      double num1 = this.ItemWidth;
      double num2 = this.ItemHeight;
      if (this.UseRelativeSize)
        num2 = this.Orientation != null ? (num1 = finalSize.Height * this.ItemRelativeSize) : (num1 = finalSize.Width * this.ItemRelativeSize);
      Point point = new Point((finalSize.Width - num1) / 2.0, (finalSize.Height - num2) / 2.0);
      for (int index = 0; index < (this.Children).Count; ++index)
      {
        FrameworkElement child = (FrameworkElement) (this.Children)[index];
        child.Width = num1;
        child.Height = num2;
        ((UIElement) child).Arrange(new Rect(point.X, point.Y, num1, num2));
        Storyboard storyboard = new Storyboard();
        double num3 = (double) (index - this.FocusedItemIndex);
        double num4 = Math.Abs(num3);
        Canvas.SetZIndex((UIElement) child, -(int) num4);
        TranslateTransform renderTransform = (TranslateTransform) ((UIElement) child).RenderTransform;
        DoubleAnimation doubleAnimation1 = new DoubleAnimation();
                doubleAnimation1.EasingFunction = default;//this.AnimationFunction;
        ((Timeline)doubleAnimation1).Duration = this.AnimationDuration;//Duration.op_Implicit(this.AnimationDuration);
        DoubleAnimation doubleAnimation2 = doubleAnimation1;
        Storyboard.SetTarget((Timeline) doubleAnimation2, (DependencyObject) renderTransform);
        
        //if (this.Orientation == null)
        //  Storyboard.SetTargetProperty((Timeline) doubleAnimation2, new PropertyPath( TranslateTransform.YProperty.ToString() ) );
        //else
        //  Storyboard.SetTargetProperty((Timeline) doubleAnimation2, new PropertyPath( TranslateTransform.XProperty.ToString() ) );
        
        doubleAnimation2.To = new double?(num4 > 1.0 ? (num3 < 0.0 ? -this.FocusedItemOffset : this.FocusedItemOffset) + num3 * this.UnfocusedItemOffset : num3 * this.FocusedItemOffset);
        PlaneProjection projection = (PlaneProjection) ((UIElement) child).Projection;
        DoubleAnimation doubleAnimation3 = new DoubleAnimation();
                doubleAnimation3.EasingFunction = default;//this.AnimationFunction;
                ((Timeline)doubleAnimation3).Duration = default;//this.AnimationDuration;//Duration.op_Implicit(this.AnimationDuration);
        DoubleAnimation doubleAnimation4 = doubleAnimation3;
        Storyboard.SetTarget((Timeline) doubleAnimation4, (DependencyObject) projection);
        //Storyboard.SetTargetProperty((Timeline) doubleAnimation4, new PropertyPath((object) PlaneProjection.GlobalOffsetZProperty));
        DoubleAnimation doubleAnimation5 = new DoubleAnimation();
                doubleAnimation5.EasingFunction = default;//this.AnimationFunction;
        ((Timeline)doubleAnimation5).Duration = this.AnimationDuration;//Duration.op_Implicit(this.AnimationDuration);
        DoubleAnimation doubleAnimation6 = doubleAnimation5;
        Storyboard.SetTarget((Timeline) doubleAnimation6, (DependencyObject) projection);
        if (num3 != 0.0)
        {
          doubleAnimation4.To = new double?(-num4 * this.UnfocusedItemDistance);
          //if (this.Orientation == null)
          //  Storyboard.SetTargetProperty((Timeline) doubleAnimation6, new PropertyPath((object) PlaneProjection.RotationXProperty));
          //else
          //  Storyboard.SetTargetProperty((Timeline) doubleAnimation6, new PropertyPath((object) PlaneProjection.RotationYProperty));
          doubleAnimation6.To = new double?(num3 < 0.0 ? -this.UnfocusedItemAngle : this.UnfocusedItemAngle);
        }
        else
        {
          doubleAnimation4.To = new double?(this.FocusedItemDistance);
          //if (this.Orientation == null)
          //  Storyboard.SetTargetProperty((Timeline) doubleAnimation6, new PropertyPath((object) PlaneProjection.RotationXProperty));
          //else
          //  Storyboard.SetTargetProperty((Timeline) doubleAnimation6, new PropertyPath((object) PlaneProjection.RotationYProperty));
          doubleAnimation6.To = new double?(this.FocusedItemAngle);
        }
        DoubleAnimation doubleAnimation7 = new DoubleAnimation();
        doubleAnimation7.EasingFunction = default;//this.AnimationFunction;
        ((Timeline) doubleAnimation7).Duration = this.AnimationDuration;
        DoubleAnimation doubleAnimation8 = doubleAnimation7;
        Storyboard.SetTarget((Timeline) doubleAnimation8, (DependencyObject) child);
        //Storyboard.SetTargetProperty((Timeline) doubleAnimation8, new PropertyPath((object) UIElement.OpacityProperty));
        doubleAnimation8.To = num4 <= (double) this.ItemVisibility ? new double?(Math.Max(0.0, 1.0 - num4 * this.ItemFading)) : new double?(0.0);
        //((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation2);
        //((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation4);
        //((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation6);
        //((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation8);
        storyboard.Begin();
      }
            return default;//((FrameworkElement) this).ArrangeOverride(finalSize);
    }

    [Category("CollectionFlow Panel")]
    public Orientation Orientation
    {
      get => (Orientation) ((DependencyObject) this).GetValue(CollectionFlowPanel.OrientationProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.OrientationProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public IEasingFunction AnimationFunction
    {
      get => (IEasingFunction) ((DependencyObject) this).GetValue(CollectionFlowPanel.AnimationFunctionProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.AnimationFunctionProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public TimeSpan AnimationDuration
    {
      get => (TimeSpan) ((DependencyObject) this).GetValue(CollectionFlowPanel.AnimationDurationProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.AnimationDurationProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double ItemWidth
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.ItemWidthProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.ItemWidthProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double ItemHeight
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.ItemHeightProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.ItemHeightProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double ItemRelativeSize
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.ItemRelativeSizeProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.ItemRelativeSizeProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public bool UseRelativeSize
    {
      get => (bool) ((DependencyObject) this).GetValue(CollectionFlowPanel.UseRelativeSizeProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.UseRelativeSizeProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double ItemFading
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.ItemFadingProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.ItemFadingProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public int ItemVisibility
    {
      get => (int) ((DependencyObject) this).GetValue(CollectionFlowPanel.ItemVisibilityProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.ItemVisibilityProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double FocusedItemDistance
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.FocusedItemDistanceProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.FocusedItemDistanceProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double UnfocusedItemDistance
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.UnfocusedItemDistanceProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.UnfocusedItemDistanceProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double FocusedItemOffset
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.FocusedItemOffsetProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.FocusedItemOffsetProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double UnfocusedItemOffset
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.UnfocusedItemOffsetProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.UnfocusedItemOffsetProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double FocusedItemAngle
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.FocusedItemAngleProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.FocusedItemAngleProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public double UnfocusedItemAngle
    {
      get => (double) ((DependencyObject) this).GetValue(CollectionFlowPanel.UnfocusedItemAngleProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.UnfocusedItemAngleProperty, (object) value);
    }

    [Category("CollectionFlow Panel")]
    public int FocusedItemIndex
    {
      get => (int) ((DependencyObject) this).GetValue(CollectionFlowPanel.FocusedItemIndexProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlowPanel.FocusedItemIndexProperty, (object) value);
    }
  }
}
