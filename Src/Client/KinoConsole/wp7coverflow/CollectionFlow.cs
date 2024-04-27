// Type: wp7coverflow.CollectionFlow
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System.ComponentModel;
using System.Windows;
using Windows.UI.Xaml;

//using System.Windows.Controls;
//using System.Windows.Media;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace wp7coverflow
{
  public partial class CollectionFlow : ItemsControl
  {
    public static readonly DependencyProperty 
            SelectedItemProperty = DependencyProperty.Register(nameof (SelectedItem), 
                typeof (object), typeof (CollectionFlow), (PropertyMetadata) null);

    public static readonly DependencyProperty 
            SelectedItemIndexProperty = DependencyProperty.Register(nameof (SelectedItemIndex),
                typeof (int), typeof (CollectionFlow),
                new PropertyMetadata(
                    new PropertyChangedCallback(CollectionFlow.SelectedItemIndexChanged)));

        public CollectionFlow()
        {
            //RnD
            //((Control)this).DefaultStyleKey = (object)typeof(CollectionFlow);
        }

        private static void SelectedItemIndexChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      CollectionFlow collectionFlow = (CollectionFlow) sender;
      if (collectionFlow.Panel == null || collectionFlow.Panel.FocusedItemIndex == collectionFlow.SelectedItemIndex)// || ((PresentationFrameworkCollection<object>) collectionFlow.Items).Count <= collectionFlow.SelectedItemIndex)
        return;
      collectionFlow.Panel.FocusedItemIndex = (int) e.NewValue;

      //(PresentationFrameworkCollection<object>) 
      collectionFlow.SelectedItem = (collectionFlow.Items)[collectionFlow.SelectedItemIndex];
    }

    protected virtual DependencyObject GetContainerForItemOverride()
    {
      if (this.Panel != null && this.Panel.FocusedItemIndex != this.SelectedItemIndex)
      {
        this.Panel.FocusedItemIndex = this.SelectedItemIndex;

                //(PresentationFrameworkCollection<object>) 
        this.SelectedItem = (this.Items)[this.SelectedItemIndex];
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

        public CollectionFlowPanel Panel
        {
            get
            {
                //RnD
                return default;//((Control)this).GetTemplateChild("ItemsPresenter") != null
                    //? (CollectionFlowPanel)VisualTreeHelper.GetChild(((Control)this).GetTemplateChild("ItemsPresenter"), 0)
                    //: (CollectionFlowPanel)null;
            }
        }
    }
}
