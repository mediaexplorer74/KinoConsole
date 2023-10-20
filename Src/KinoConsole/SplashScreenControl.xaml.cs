// Decompiled with JetBrains decompiler
// Type: KinoConsole.SplashScreenControl
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System;
using System.Diagnostics;
using System.Windows;
//using System.Windows.Controls;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace KinoConsole
{
    public sealed partial class SplashScreenControl : UserControl
    {
        //internal Grid LayoutRoot;
        //internal ProgressBar progressBar1;
        //private bool _contentLoaded;

        public SplashScreenControl()
        {
            this.InitializeComponent();
        }

        /*
        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/KinoConsole;component/SplashScreenControl.xaml", UriKind.Relative));
            this.LayoutRoot = (Grid)((FrameworkElement)this).FindName("LayoutRoot");
            this.progressBar1 = (ProgressBar)((FrameworkElement)this).FindName("progressBar1");
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
    public sealed partial class SplashScreenControl : UserControl
    {
        public SplashScreenControl()
        {
            this.InitializeComponent();
        }
    }
}
*/
