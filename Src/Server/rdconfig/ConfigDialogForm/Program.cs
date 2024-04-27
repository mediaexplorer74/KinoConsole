// Decompiled with JetBrains decompiler
// Type: ConfigDialogForm.Program
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace ConfigDialogForm
{
  internal static class Program
  {
    private static Mutex mutex = new Mutex(false, "com.kinoni.remotedesktop.config");

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [STAThread]
    private static void Main(string[] args)
    {
      if (!Program.mutex.WaitOne(TimeSpan.FromSeconds(2.0), false))
      {
        Process[] processesByName = Process.GetProcessesByName("KinoConsole");
        if (processesByName.Length > 0)
          Program.SetForegroundWindow(processesByName[0].MainWindowHandle);
        Console.WriteLine("Another instance of the app is running. Bye!");
      }
      else
      {
        try
        {
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          Application.Run((Form) new Form1(args));
        }
        finally
        {
          Program.mutex.ReleaseMutex();
        }
      }
    }
  }
}
