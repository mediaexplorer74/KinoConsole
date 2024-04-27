// Decompiled with JetBrains decompiler
// Type: KinoConsole.ButtonDialog
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;


namespace KinoConsole
{
    public sealed partial class ButtonDialog : UserControl
    {
        //internal StackPanel LayoutRoot;
        //internal ListBox buttonPicker;
        //internal Button btnDlgCancel;
        //private bool _contentLoaded;

        public ButtonDialog()
        {
            this.InitializeComponent();
            //TiltEffect.TiltableItems.Add(typeof(StackPanel));
        }

        /*
        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/KinoConsole;component/ButtonDialog.xaml", UriKind.Relative));
            this.LayoutRoot = (StackPanel)((FrameworkElement)this).FindName("LayoutRoot");
            this.buttonPicker = (ListBox)((FrameworkElement)this).FindName("buttonPicker");
            this.btnDlgCancel = (Button)((FrameworkElement)this).FindName("btnDlgCancel");
        }
        */
    }
}

/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace KinoConsole
{
    public sealed partial class ButtonDialog : UserControl
    {
        public ButtonDialog()
        {
            this.InitializeComponent();
        }
    }
}
*/
