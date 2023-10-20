﻿// Decompiled with JetBrains decompiler
// Type: wp7coverflow.CollectionFlow
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace wp7coverflow
{
  public class CollectionFlow : ItemsControl
  {
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof (SelectedItem), typeof (object), typeof (CollectionFlow), (PropertyMetadata) null);
    public static readonly DependencyProperty SelectedItemIndexProperty = DependencyProperty.Register(nameof (SelectedItemIndex), typeof (int), typeof (CollectionFlow), new PropertyMetadata(new PropertyChangedCallback(CollectionFlow.SelectedItemIndexChanged)));

    public CollectionFlow() => ((Control) this).DefaultStyleKey = (object) typeof (CollectionFlow);

    private static void SelectedItemIndexChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      CollectionFlow collectionFlow = (CollectionFlow) sender;
      if (collectionFlow.Panel == null || collectionFlow.Panel.FocusedItemIndex == collectionFlow.SelectedItemIndex || ((PresentationFrameworkCollection<object>) collectionFlow.Items).Count <= collectionFlow.SelectedItemIndex)
        return;
      collectionFlow.Panel.FocusedItemIndex = (int) e.NewValue;
      collectionFlow.SelectedItem = ((PresentationFrameworkCollection<object>) collectionFlow.Items)[collectionFlow.SelectedItemIndex];
    }

    protected virtual DependencyObject GetContainerForItemOverride()
    {
      if (this.Panel != null && this.Panel.FocusedItemIndex != this.SelectedItemIndex)
      {
        this.Panel.FocusedItemIndex = this.SelectedItemIndex;
        this.SelectedItem = ((PresentationFrameworkCollection<object>) this.Items)[this.SelectedItemIndex];
      }
      return base.GetContainerForItemOverride();
    }

    [Category("CollectionFlow Panel")]
    public object SelectedItem
    {
      get => ((DependencyObject) this).GetValue(CollectionFlow.SelectedItemProperty);
      private set => ((DependencyObject) this).SetValue(CollectionFlow.SelectedItemProperty, value);
    }

    [Category("CollectionFlow Panel")]
    public int SelectedItemIndex
    {
      get => (int) ((DependencyObject) this).GetValue(CollectionFlow.SelectedItemIndexProperty);
      set => ((DependencyObject) this).SetValue(CollectionFlow.SelectedItemIndexProperty, (object) value);
    }

    public CollectionFlowPanel Panel => ((Control) this).GetTemplateChild("ItemsPresenter") != null ? (CollectionFlowPanel) VisualTreeHelper.GetChild(((Control) this).GetTemplateChild("ItemsPresenter"), 0) : (CollectionFlowPanel) null;
  }
}