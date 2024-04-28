// Decompiled with JetBrains decompiler
// Type: ConfigDialogForm.Form1
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using BrightIdeasSoftware;
using ConfigDialogForm.Properties;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using Shell32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using TAFactory.IconPack;


namespace ConfigDialogForm
{
  public class Form1 : Form
  {
    private const string configXmlFile = "\\Kinoni\\RemoteDesktop\\kinoconsole2.xml";
    private const int GOOGLE_LOGGED_IN = 1;
    private const int GOOGLE_LOGGED_OUT = 2;
    private const int GOOGLE_LOGIN_FAILED = 3;
    private const string steamBPMCommand = "steam://open/bigpicture";
    private const int serverConfigSize = 1064;
    private List<Form1.AppInfo> apps;
    private Font TitleFont;
    private Font regularFont;
    private int selectedIndex = -1;
    private int newSteamApps;
    private bool autoscansteam;
    private bool kinoconsole;
    private ListView.SelectedIndexCollection selectedIndices;
    private AlertForm alert;
    private BackgroundWorker scanBw;
    private BackgroundWorker ipBw;
    private BackgroundWorker applistBw;
    private string externalIP;
    private int recentVersion;
    private byte[] password = new byte[32];
    private uint[] version = new uint[1];
    private char[] googleName = new char[256];
    private char[] googlePw = new char[256];
    private int googleLoginStatus = 2;
    private int googleWantedStatus;
    private byte[] guid;
    private IContainer components;
    private ObjectListView objectListView1;
    private OLVColumn iconColumn;
    private OLVColumn nameColumn;
    private OLVColumn launchCmdColumn;
    private OLVColumn enabledColumn;
    private ImageList iconList;
    private ImageList largeIconList;
    private Button OKbutton;
    private Button cancelButton;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private Label label1;
    private GroupBox groupBox4;
    private GroupBox groupBox3;
    private GroupBox groupBox2;
    private Label label2;
    private GroupBox groupBox1;
    private Label label7;
    private TextBox googlePasswordTextBox;
    private Label label6;
    private TextBox googleUsernameTextBox;
    private Label label5;
    private Label computerNameLabel;
    private Label label8;
    private TextBox retypePasswordTextBox;
    private TextBox passwordTextBox;
    private Label label4;
    private Label label3;
    private Button googleLoginButton;
    private Label googleStatusLabel;
    private Button browseButton;
    private Button deleteButton;
    private Label label11;
    private Button updateAppButton;
    private PictureBox pictureBox1;
    private Button steamScanButton;
    private PictureBox pwdStatusPictureBox;
    private LinkLabel linkLabel1;
    private Label externalIPlabel;
    private Label label9;
    private ComboBox XBoxCombo;
    private TabPage tabPage3;
    private GroupBox groupBox5;
    private ComboBox audioDeviceCombo;
    private CheckBox muteBox;
    private GroupBox groupBox6;
    private Label label10;

    [DllImport("config.dll")]
    public static extern int UpdateAvailable(int recentVersion);

    [DllImport("config.dll")]
    public static extern int AudioDeviceCount();

    [DllImport("config.dll")]
    public static extern void AudioDeviceName(int idx, out IntPtr ppName);

    [DllImport("config.dll")]
    public static extern int GetCurrentAudioDevice();

    [DllImport("config.dll")]
    public static extern void SetCurrentAudioDevice(int idx);

    public object GameImageGetter(object rowObject)
    {
      return (object) ((Form1.AppInfo) rowObject).iconListIndex;
    }

    private void objectListView1_FormatCell(object sender, FormatCellEventArgs e)
    {
      if (e.ColumnIndex == this.nameColumn.Index)
        e.SubItem.Font = this.TitleFont;
      else if (e.ColumnIndex == this.enabledColumn.Index)
      {
        if ((bool) e.CellValue)
          e.SubItem.ForeColor = System.Drawing.Color.Green;
        else
          e.SubItem.ForeColor = System.Drawing.Color.Red;
        e.SubItem.Font = this.regularFont;
      }
      else if (e.ColumnIndex == this.launchCmdColumn.Index)
      {
        if (((Form1.AppInfo) e.Model).steamApp)
          e.SubItem.ForeColor = System.Drawing.Color.Gray;
        e.SubItem.Font = this.regularFont;
      }
      else
        e.SubItem.Font = this.regularFont;
    }

    private void Form1_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
      Console.WriteLine(e.X);
    }

    private void Form1_DragDrop(object sender, DragEventArgs e)
    {
      foreach (string str in (string[]) e.Data.GetData(DataFormats.FileDrop))
        Console.WriteLine(str);
    }

    private void SaveDialogConfig()
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this.version[0]);
          if (this.passwordTextBox.Text.Equals(this.retypePasswordTextBox.Text))
          {
            Array.Clear((Array) this.password, 0, 32);
            char[] charArray = this.passwordTextBox.Text.ToCharArray();
            Buffer.BlockCopy((Array) Encoding.ASCII.GetBytes(charArray), 0, (Array) this.password, 0, charArray.Length);
          }
          binaryWriter.Write(this.password);
          binaryWriter.Write(this.googleWantedStatus);
          string text1 = this.googleUsernameTextBox.Text;
          Array.Clear((Array) this.googleName, 0, 256);
          Buffer.BlockCopy((Array) text1.ToCharArray(), 0, (Array) this.googleName, 0, text1.Length * 2);
          byte[] bytes1 = Encoding.Unicode.GetBytes(this.googleName);
          binaryWriter.Write(bytes1);
          string text2 = this.googlePasswordTextBox.Text;
          Array.Clear((Array) this.googlePw, 0, 256);
          Buffer.BlockCopy((Array) text2.ToCharArray(), 0, (Array) this.googlePw, 0, text2.Length * 2);
          byte[] bytes2 = Encoding.Unicode.GetBytes(this.googlePw);
          binaryWriter.Write(bytes2);
        }
        output.Flush();
        byte[] array = output.ToArray();
        Form1.EncryptCfg(ref array, 1064, this.guid);
        try
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Kinoni", "rdcfg", 
                (object) array, RegistryValueKind.Binary);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] Registry.SetValue - rdcfg error: " + ex.Message);
        }
        RegistryKey localMachine = Registry.LocalMachine;
        try
        {
          Registry.LocalMachine.DeleteValue("Software\\Kinoni\\rdpw");
          Registry.LocalMachine.DeleteValue("Software\\Kinoni\\rdglogin");
          Registry.LocalMachine.DeleteValue("Software\\Kinoni\\rdgname");
          Registry.LocalMachine.DeleteValue("Software\\Kinoni\\rdgpw");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] Registry.DeleteValue - rdpw error: " + ex.Message);
        }
       }
    }

    private void LoadDialogConfig()
    {
      int num1 = 21;
      this.computerNameLabel.Text = Environment.MachineName.ToString();
      string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Kinoni";
        
        try
        {
            Registry.SetValue(keyName, "rdver", (object)num1, RegistryValueKind.DWord);
        }
        catch { }

        try
        {
            this.guid = (byte[])Registry.GetValue(keyName, "rdid", (object)null);
        }
        catch { }

        byte[] byteArray = Guid.NewGuid().ToByteArray();
        if (this.guid == null)
        {
          try
          {
             Registry.SetValue(keyName, "rdid", (object)byteArray, RegistryValueKind.Binary);
          }
          catch { }
          this.guid = byteArray;
        }

        int srcOffset1 = 0;
        byte[] data = default;
        try
        {
            data = (byte[])Registry.GetValue(keyName, "rdcfg", (object)null);
        }
        catch { }

        if (data == null)
          return;

        this.DecryptCfg(ref data, 1064, this.guid);
        int count1 = 4;
        Buffer.BlockCopy((Array) data, srcOffset1, (Array) this.version, 0, count1);
        int srcOffset2 = srcOffset1 + count1;
        int count2 = 32;
        Buffer.BlockCopy((Array) data, srcOffset2, (Array) this.password, 0, count2);
        int srcOffset3 = srcOffset2 + count2;
        int count3 = 4;
        uint[] dst = new uint[1];
        Buffer.BlockCopy((Array) data, srcOffset3, (Array) dst, 0, count3);
        int srcOffset4 = srcOffset3 + count3;
        int count4 = 512;
        Buffer.BlockCopy((Array) data, srcOffset4, (Array) this.googleName, 0, count4);
        int srcOffset5 = srcOffset4 + count4;
        int count5 = 512;
        Buffer.BlockCopy((Array) data, srcOffset5, (Array) this.googlePw, 0, count5);
        int num2 = srcOffset5 + count5;
        string str = Encoding.Default.GetString(this.password);
        this.passwordTextBox.Text = str;
        this.retypePasswordTextBox.Text = str;
        this.googleUsernameTextBox.Text = new string(this.googleName);
        this.googlePasswordTextBox.Text = new string(this.googlePw);
      //}
      //catch (Exception ex)
      //{
      //}
      }

    private void DecryptCfg(ref byte[] data, int size, byte[] key)
    {
      for (int index = size - 1; index > 0; --index)
        data[index] ^= data[index - 1];
      for (int index = 0; index < size; ++index)
        data[index] = (byte) ((int) data[index] ^ (int) key[index % 15] ^ index % 256);
    }

    private static void EncryptCfg(ref byte[] data, int size, byte[] key)
    {
      for (int index = 0; index < size; ++index)
        data[index] = (byte) ((int) data[index] ^ (int) key[index % 15] ^ index % 256);
      for (int index = 1; index < size; ++index)
        data[index] = (byte) ((uint) data[index - 1] ^ (uint) data[index]);
    }

    private void LoadAppConfig()
    {
      XmlDocument xmlDocument = new XmlDocument();
      string str1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Kinoni\\RemoteDesktop\\kinoconsole2.xml";
      if (!System.IO.File.Exists(str1))
        return;
      xmlDocument.Load(str1);
      foreach (XmlNode xmlNode in xmlDocument.GetElementsByTagName("AppInfo"))
      {
        string appName = xmlNode.Attributes["name"].Value;
        string appPath = xmlNode.Attributes["path"].Value;
        string folder = xmlNode.Attributes["folder"].Value;
        string str2 = xmlNode.Attributes["iconpath"].Value;
        bool boolean1 = Convert.ToBoolean(xmlNode.Attributes["steam"].Value);
        bool boolean2 = Convert.ToBoolean(xmlNode.Attributes["enabled"].Value);
        Icon icon = this.ExtractIcon(str2);
        this.iconList.Images.Add(icon);
        this.largeIconList.Images.Add(icon);
        int iconListIndex = this.iconList.Images.Count - 1;
        this.apps.Add(new Form1.AppInfo(appName, appPath, folder, str2, boolean2, iconListIndex, boolean1));
      }
      this.objectListView1.SetObjects((IEnumerable) this.apps);
    }

    private void SaveAppConfig()
    {
      XmlDocument xmlDocument = new XmlDocument();

      XmlNode xmlDeclaration = (XmlNode) xmlDocument.CreateXmlDeclaration("1.0", (string) null, (string) null);

      xmlDocument.AppendChild(xmlDeclaration);

      XmlElement xmlElement = 
                (XmlElement) xmlDocument.AppendChild((XmlNode) xmlDocument.CreateElement("KinoConsole"));

      xmlElement.SetAttribute("version", "1");
      string path1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Kinoni";
      string path2 = path1 + "\\RemoteDesktop";
      string path3 = path2 + "\\appinfo.bin";
      try
      {
        Directory.CreateDirectory(path1);
        Directory.CreateDirectory(path2);
        System.IO.File.Delete(path3);
      }
      catch (Exception ex)
      {
      }
      BinaryWriter binaryWriter = new BinaryWriter((Stream) new FileStream(path3, FileMode.Create));
      byte num1 = 20;
      foreach (Form1.AppInfo app in this.apps)
      {
        XmlElement element = xmlDocument.CreateElement("AppInfo");
        XmlAttribute attribute1 = xmlDocument.CreateAttribute("name");
        attribute1.Value = app.appName;
        element.Attributes.Append(attribute1);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("path");
        attribute2.Value = app.appPath;
        element.Attributes.Append(attribute2);
        XmlAttribute attribute3 = xmlDocument.CreateAttribute("folder");
        attribute3.Value = app.folder;
        element.Attributes.Append(attribute3);
        XmlAttribute attribute4 = xmlDocument.CreateAttribute("iconpath");
        attribute4.Value = app.appIconPath;
        element.Attributes.Append(attribute4);
        XmlAttribute attribute5 = xmlDocument.CreateAttribute("steam");
        attribute5.Value = app.steamApp ? "true" : "false";
        element.Attributes.Append(attribute5);
        XmlAttribute attribute6 = xmlDocument.CreateAttribute("enabled");
        attribute6.Value = app.appEnabled ? "true" : "false";
        element.Attributes.Append(attribute6);
        xmlElement.AppendChild((XmlNode) element);
        if (app.appEnabled)
        {
          Image image = this.largeIconList.Images[app.iconListIndex];
          MemoryStream memoryStream = new MemoryStream();
          image.Save((Stream) memoryStream, ImageFormat.Png);
          byte[] array = memoryStream.ToArray();
          int num2 = 1;
          ushort num3 = 4;
          ushort num4 = 1152;
          ushort num5 = (ushort) ((uint) num4 - (uint) num3);
          int num6 = array.Length;
          if (528 + array.Length > (int) num4)
          {
            num2 = 2;
            num6 = array.Length - ((int) num4 - 528);
            while (num6 > (int) num5)
            {
              num6 -= (int) num5;
              ++num2;
            }
          }
          int index1 = 0;
          for (int index2 = 0; index2 < num2; ++index2)
          {
            ushort num7 = 528;
            byte num8 = 3;
            int count = array.Length;
            if (num2 > 1)
            {
              if (index2 == 0)
              {
                num8 = (byte) 1;
                count = (int) num4 - 528;
                num7 = (ushort) 528;
              }
              else if (index2 == num2 - 1)
              {
                count = num6;
                num7 = num3;
                num8 = (byte) 2;
              }
              else
              {
                count = (int) num5;
                num7 = num3;
                num8 = (byte) 0;
              }
            }
            ushort num9 = (ushort) ((uint) num7 + (uint) (ushort) count);
            binaryWriter.Write(num9);
            binaryWriter.Write(num1);
            binaryWriter.Write(num8);
            if (index2 == 0)
            {
              uint num10 = 0;
              binaryWriter.Write(num10);
              byte[] numArray1 = new byte[256];
              byte[] bytes = Encoding.UTF8.GetBytes(app.appName);
              Buffer.BlockCopy((Array) bytes, 0, (Array) numArray1, 0, bytes.Length);
              binaryWriter.Write(numArray1);
              byte[] numArray2 = new byte[260];
              Buffer.BlockCopy((Array) Encoding.UTF8.GetBytes(app.appPath), 0, (Array) numArray2, 0, app.appPath.Length);
              binaryWriter.Write(numArray2);
              binaryWriter.Write(count);
            }
            binaryWriter.Write(array, index1, count);
            index1 += count;
          }
        }
      }
      string filename = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Kinoni\\RemoteDesktop\\kinoconsole2.xml";
      xmlDocument.Save(filename);
      binaryWriter.Write((ushort) 528);
      binaryWriter.Write(num1);
      binaryWriter.Write((byte) 3);
      binaryWriter.Write(0U);
      byte[] buffer1 = new byte[256];
      binaryWriter.Write(buffer1);
      byte[] buffer2 = new byte[260];
      binaryWriter.Write(buffer2);
      binaryWriter.Write(0U);
      binaryWriter.Close();
    }

    private int GetGoogleStatus()
    {
      int googleStatus = 2;
      try
      {
        object status;  
        status =  Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Kinoni", "rdgstatus", null);
        if (status != null)
            googleStatus = (int) status;
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] Form1, Registry.GetValue error: " + ex.Message);
      }      
      return googleStatus;
    }

    public Form1(string[] cmdlineargs)
    {
      foreach (string cmdlinearg in cmdlineargs)
      {
        if (cmdlinearg.Equals("--scansteam"))
          this.autoscansteam = true;

        if (cmdlinearg.Equals("--kinoconsole"))
          this.kinoconsole = true;

        Debug.WriteLine("[i] cmdlinearg :" + cmdlinearg);
      }

      this.InitializeComponent();
      
      this.iconList = new ImageList();
      this.largeIconList = new ImageList();
      this.TitleFont = new Font("Segoe UI", 12f, FontStyle.Regular);
      this.regularFont = new Font("Segoe UI", 10f, FontStyle.Regular);
      this.ipBw = new BackgroundWorker();
      this.ipBw.WorkerSupportsCancellation = false;
      this.ipBw.WorkerReportsProgress = false;
      this.ipBw.DoWork += new DoWorkEventHandler(this.ipbw_DoWork);
      this.ipBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.ipbw_RunWorkerCompleted);
      this.externalIPlabel.Text = "N/A";
      this.applistBw = new BackgroundWorker();
      this.applistBw.WorkerSupportsCancellation = false;
      this.applistBw.WorkerReportsProgress = false;
      this.applistBw.DoWork += new DoWorkEventHandler(this.applistBw_DoWork);
      this.applistBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.applistBw_RunWorkerCompleted);
      try
      {
        this.apps = new List<Form1.AppInfo>();
        this.iconList.ImageSize = new Size(32, 32);
        this.iconList.ColorDepth = ColorDepth.Depth32Bit;
        this.largeIconList.ImageSize = new Size(256, 256);
        this.largeIconList.ColorDepth = ColorDepth.Depth32Bit;
        this.updateAppButton.Visible = false;

        this.LoadDialogConfig();
        this.googleLoginStatus = this.GetGoogleStatus();
        this.googleWantedStatus = this.googleLoginStatus == 1 ? 1 : 0;

        if (this.googleLoginStatus == 1)
        {
          this.googleStatusLabel.Text = "Status: Logged in";
          this.googleStatusLabel.ForeColor = System.Drawing.Color.Green;
          this.googleLoginButton.Text = "Log out";
        }
        else
        {
          this.googleStatusLabel.Text = "Status: Logged out";
          this.googleStatusLabel.ForeColor = System.Drawing.Color.Red;
          this.googleLoginButton.Text = "Log in";
        }
        this.linkLabel1.Links.Add(new LinkLabel.Link()
        {
          LinkData = (object) "http://www.microsoft.com/hardware/en-us/d/xbox-360-controller-for-windows"
        });

        this.linkLabel1.LinkClicked += (LinkLabelLinkClickedEventHandler) ((sender, e) =>
        {
            Process.Start(e.Link.LinkData as string);
        });

        new ToolTip().SetToolTip((Control) this.linkLabel1,
            "Installing Xbox 360 drivers allows using joystick to play games");
        this.linkLabel1.Visible = false;

        int si = -1;

        try
        {
            object o = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Kinoni", "rdxbox", (object)0);
            if (o != null)
                si = (int)o;
        }
        catch (Exception ex)
        {
           Debug.WriteLine("[ex] Registry.GetValue - rdxbox - error: " + ex.Message);
        }

        this.XBoxCombo.SelectedIndex = si;
        int num = Form1.AudioDeviceCount();
        for (int idx = 0; idx < num; ++idx)
        {
          IntPtr ppName;
          Form1.AudioDeviceName(idx, out ppName);
          this.audioDeviceCombo.Items.Add((object) Marshal.PtrToStringUni(ppName));
        }
        this.audioDeviceCombo.SelectedIndex = Form1.GetCurrentAudioDevice();
        int aaa = 0;
        try
        {
            object ooo = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Kinoni", "rdmute", (object)0);
            if (ooo != null)
                aaa = (int)ooo;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] Registry.GetValue - rdmute - error: " + ex.Message);
        }

        this.muteBox.Checked = aaa != 0;
      }
      catch (Exception ex2)
      {
                Debug.WriteLine("[ex2] Registry.GetValue - rdmute - error: " + ex2.Message);
      }
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.objectListView1.SetObjects((IEnumerable) this.apps);
      this.objectListView1.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
      this.objectListView1.SmallImageList = this.iconList;
      this.objectListView1.HeaderFont = new Font("Segoe UI", 14f, FontStyle.Regular);
      this.objectListView1.AddDecoration((IDecoration) new EditingCellBorderDecoration()
      {
        UseLightbox = true
      });

      this.objectListView1.FullRowSelect = true;
      this.objectListView1.MultiSelect = true;
      this.objectListView1.ShowGroups = false;
      this.AllowDrop = true;
      this.objectListView1.EmptyListMsg = "Loading saved configuration, please wait..";
      this.DragEnter += new DragEventHandler(this.Form1_DragEnter);
      this.DragDrop += new DragEventHandler(this.Form1_DragDrop);
      this.objectListView1.IsSimpleDropSink = true;
      this.objectListView1.DropSink = (IDropSink) new SimpleDropSink()
      {
        CanDropOnItem = true,
        CanDropOnSubItem = true,
        CanDropOnBackground = true,
        FeedbackColor = System.Drawing.Color.Green
      };

      this.objectListView1.CanDrop += (EventHandler<OlvDropEventArgs>) ((sender, e) =>
      {
        if (e.DropTargetItem != null)
        {
          Form1.AppInfo rowObject = (Form1.AppInfo) e.DropTargetItem.RowObject;
          Debug.WriteLine("index: " + (object) e.DropTargetIndex + " item: " + (object) rowObject);
        }
        if (e.DropTargetIndex == -1 || e.DropTargetSubItemIndex == this.launchCmdColumn.Index 
          || e.DropTargetSubItemIndex == this.nameColumn.Index)
        {
          e.Effect = DragDropEffects.Copy;
          e.InfoMessage = "Add new entry";
        }
        else if (e.DropTargetSubItemIndex == this.iconColumn.Index)
        {
          e.Effect = DragDropEffects.Copy;
          e.InfoMessage = "Replace icon";
        }
        else
        {
          if (e.DropTargetSubItemIndex != this.enabledColumn.Index)
            return;
          e.Effect = DragDropEffects.None;
        }
      });

      this.objectListView1.Dropped += (EventHandler<OlvDropEventArgs>) ((sender, e) =>
      {
        string[] data = (string[]) e.DragEventArgs.Data.GetData(DataFormats.FileDrop);
        if (e.DropTargetItem != null && e.DropTargetSubItemIndex == this.iconColumn.Index)
        {
          Form1.AppInfo rowObject = (Form1.AppInfo) e.DropTargetItem.RowObject;
          foreach (Form1.AppInfo app in this.apps)
          {
            if (app.appPath.Equals(rowObject.appPath))
            {
              Icon icon = this.ExtractIcon(data[0]);
              this.iconList.Images.Add(icon);
              this.largeIconList.Images.Add(icon);
              int num = this.iconList.Images.Count - 1;
              app.iconListIndex = num;
              break;
            }
          }
          this.objectListView1.RefreshObjects((IList) this.apps);
        }
        else
        {
          if (e.DropTargetSubItemIndex != this.launchCmdColumn.Index && e.DropTargetSubItemIndex != this.iconColumn.Index && e.DropTargetSubItemIndex != this.nameColumn.Index && e.DropTargetItem != null)
            return;
          foreach (string path in data)
            this.AddEntry(path);
        }
      });

      this.objectListView1.SelectedIndexChanged += (EventHandler) ((sender, e) =>
      {
        this.selectedIndices = this.objectListView1.SelectedIndices;
        this.selectedIndex = this.objectListView1.SelectedIndex;
        if (this.selectedIndex == -1 && this.selectedIndices.Count == 0)
          this.deleteButton.Enabled = false;
        else
          this.deleteButton.Enabled = true;
      });
      this.objectListView1.CellEditStarting += (CellEditEventHandler) ((sender, e) =>
      {
        if (this.apps[this.selectedIndex].steamApp && e.Column == this.launchCmdColumn)
        {
          e.Cancel = true;
        }
        else
        {
          if (e.Column != this.iconColumn)
            return;
          e.Cancel = true;
        }
      });
      this.objectListView1.BeforeSorting += (EventHandler<BeforeSortingEventArgs>) ((sender, args) =>
      {
        args.Canceled = true;
        if (args.ColumnToSort == this.nameColumn)
          return;
        args.Canceled = true;
      });

      string str = (string) Registry.GetValue(
          "HKEY_LOCAL_MACHINE\\SOFTWARE\\Classes\\Steam\\Shell\\Open\\Command", (string) null, (object) null);
      if (str == null)
        this.steamScanButton.Visible = false;
      if (this.kinoconsole)
      {
        this.Text = "KinoConsole";
        this.pictureBox1.BackgroundImage = (Image) Resources.consoleicon512;
      }
      else
        this.tabControl1.Controls.Remove((Control) this.tabPage1);

      if (this.autoscansteam && str != null)
        this.steamScanButton_Click((object) null, (EventArgs) null);

      this.scanBw = new BackgroundWorker();
      this.scanBw.WorkerSupportsCancellation = true;
      this.scanBw.WorkerReportsProgress = true;
      this.scanBw.DoWork += new DoWorkEventHandler(this.bw_DoWork);
      this.scanBw.ProgressChanged += new ProgressChangedEventHandler(this.bw_ProgressChanged);
      this.scanBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);

      if (this.kinoconsole)
        this.applistBw.RunWorkerAsync();

      this.ipBw.RunWorkerAsync();
    }

    private void OKbutton_Click(object sender, EventArgs e)
    {
        try
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Kinoni",
                "rdxbox", (object)this.XBoxCombo.SelectedIndex, RegistryValueKind.DWord);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] Registry.SetValue - rdxbox - error: " + ex.Message);
        }

        Form1.SetCurrentAudioDevice(this.audioDeviceCombo.SelectedIndex);

        try
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Kinoni", "rdmute",
                (object) this.muteBox.Checked, RegistryValueKind.DWord);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] Registry.SetValue - rdmute - error: " + ex.Message);
        }

        this.SaveDialogConfig();
        this.SaveAppConfig();
        this.StopServer();
        Application.Exit();
    }


    // googleLoginButton_Click
    private void googleLoginButton_Click(object sender, EventArgs e)
    {
      this.googleLoginButton.Enabled = false;
      this.googleStatusLabel.Visible = false;

      if (this.googleWantedStatus == 1 && this.googleLoginStatus == 1)
        this.googleWantedStatus = 0;
      else if (this.googleWantedStatus == 0 && this.googleLoginStatus == 2)
        this.googleWantedStatus = 1;
      
      this.SaveDialogConfig();
      this.StopServer();

      SynchronizationContext syncContext = SynchronizationContext.Current;

      new System.Timers.Timer()
      {
        Interval = 2000.0,
        Enabled = true,
        AutoReset = true
      }.Elapsed += (ElapsedEventHandler) ((param0, param1) =>
      {
        this.googleLoginStatus = this.GetGoogleStatus();

        syncContext.Send((SendOrPostCallback) (state =>
        {
          if (this.googleLoginStatus == 1)
          {
            this.googleStatusLabel.Text = "Status: Logged in";
            this.googleStatusLabel.ForeColor = System.Drawing.Color.Green;
            this.googleLoginButton.Text = "Log out";
          }
          else if (this.googleLoginStatus == 2)
          {
            this.googleStatusLabel.Text = "Status: Logged out";
            this.googleStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.googleLoginButton.Text = "Log in";
          }
          else if (this.googleLoginStatus == 3)
          {
            this.googleStatusLabel.Text = "Status: Login failed";
            this.googleStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.googleLoginButton.Text = "Log in";
          }
          else
          {
            this.googleStatusLabel.Text = "";
            this.googleStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.googleLoginButton.Text = "Log in";
          }

          this.googleLoginButton.Enabled = true;
          this.googleStatusLabel.Visible = true;
        }), 
        (object) null);
      });
    }

    private void StopServer()
    {
      try
      {
        EventWaitHandle eventWaitHandle = EventWaitHandle.OpenExisting("Global\\KinoniRDEvent", 
            EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize);
        eventWaitHandle.Set();
        eventWaitHandle.Close();
      }
      catch (Exception ex)
      {
         Debug.WriteLine("[ex] StopServer error: " + ex.Message);
      }
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private Icon ExtractIcon(string iconPath)
    {
      Icon icon1 = Resources.remove_256;
      try
      {
        Icon icon2 = IconHelper.ExtractIcon(iconPath, 0);
        icon1 = icon2;
        foreach (Icon icon3 in IconHelper.SplitGroupIcon(icon2))
        {
          if (icon3.Size.Width > icon1.Width)
            icon1 = icon3;
        }
      }
      catch (Exception ex1)
      {
        Debug.WriteLine("[i] " + ex1.Message);
        try
        {
          Shell shell = (Shell) new ShellClass();
          string fullPath = Path.GetFullPath(iconPath);
          string pbs;

          ((IShellLinkDual2) ((IShellDispatch6) shell).NameSpace(
              (object) Path.GetDirectoryName(fullPath)).Items().Item(
              (object) Path.GetFileName(fullPath)).GetLink).GetIconLocation(out pbs);

          string str = "file://";
          string fileName = Uri.UnescapeDataString(pbs);
          if (fileName.IndexOf(str) == 0)
            fileName = fileName.Substring(str.Length + 1);
          Icon icon4 = IconHelper.ExtractIcon(fileName, 0);
          icon1 = icon4;
          foreach (Icon icon5 in IconHelper.SplitGroupIcon(icon4))
          {
            if (icon5.Size.Width > icon1.Width)
              icon1 = icon5;
          }
        }
        catch (Exception ex2)
        {
           Debug.WriteLine("[i] " + ex2.Message);
        }
      }
      return icon1;
    }

    private void AddEntry(string path)
    {
      FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);
      for (int index = 0; index < this.apps.Count; ++index)
      {
        if (this.apps[index].appPath.Equals(path))
          return;
      }
      string str = path;
      string appName;

      if (versionInfo.FileDescription != null
                && !string.IsNullOrEmpty(versionInfo.FileDescription)
                && !string.Equals(versionInfo.FileDescription, " "))
      {
        appName = versionInfo.FileDescription;
      }
      else
      {
        int startIndex = str.LastIndexOf("\\") + 1;
        int num = str.LastIndexOf('.');
        if (num == -1)
          num = str.Length;
        int length = num - startIndex;
        appName = str.Substring(startIndex, length);
      }
      int count = this.apps.Count;
      Icon icon = this.ExtractIcon(path);
      this.iconList.Images.Add(icon);
      this.largeIconList.Images.Add(icon);
      int iconListIndex = this.iconList.Images.Count - 1;
      this.apps.Add(new Form1.AppInfo(appName, path, path, path, true, iconListIndex, false));
      this.apps.Sort();
      this.objectListView1.SetObjects((IEnumerable) this.apps);
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.CheckFileExists = false;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.AddEntry(openFileDialog.FileName);
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      int selectedIndex = this.objectListView1.SelectedIndex;
      if (selectedIndex != -1)
        this.apps.RemoveAt(selectedIndex);
      else if (this.selectedIndices.Count > 0)
      {
        foreach (Form1.AppInfo selectedObject in (IEnumerable) this.objectListView1.SelectedObjects)
          this.apps.Remove(selectedObject);
      }
      this.objectListView1.SetObjects((IEnumerable) this.apps);
      this.selectedIndex = -1;
      this.deleteButton.Enabled = false;
    }

    private void EnableButton(bool enabled)
    {
      if (this.steamScanButton.InvokeRequired)
        this.Invoke((Delegate) new Form1.EnableButtonCallback(this.EnableButton), (object) enabled);
      else
        this.steamScanButton.Enabled = enabled;
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.scanBw.CancelAsync();
      this.alert.Close();
    }

    private void steamScanButton_Click(object sender, EventArgs e)
    {
      this.alert = new AlertForm();
      this.alert.Canceled += new EventHandler<EventArgs>(this.buttonCancel_Click);
      this.alert.okButton.Visible = false;
      this.steamScanButton.Enabled = false;
      this.newSteamApps = 0;
      this.scanBw.RunWorkerAsync();
      int num = (int) this.alert.ShowDialog();
    }

    private void updateAppButton_Click(object sender, EventArgs e)
    {
      Updateform updateform = new Updateform();
      if (this.kinoconsole)
        updateform.link = "http://www.kinoni.com/kdrivers";
      int num = (int) updateform.ShowDialog();
    }

    private void passwordText_Changed(object sender, EventArgs e)
    {
      if (this.passwordTextBox.Text.Equals(this.retypePasswordTextBox.Text))
        this.pwdStatusPictureBox.Image = (Image) Resources.button_ok;
      else
        this.pwdStatusPictureBox.Image = (Image) Resources.button_nok;
    }

    private void repasswordText_Changed(object sender, EventArgs e)
    {
      if (this.passwordTextBox.Text.Equals(this.retypePasswordTextBox.Text))
        this.pwdStatusPictureBox.Image = (Image) Resources.button_ok;
      else
        this.pwdStatusPictureBox.Image = (Image) Resources.button_nok;
    }

    private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      string str = "No games added";
      if (this.newSteamApps == 1)
        str = "Added 1 game";
      else if (this.newSteamApps > 1)
        str = "Added " + (object) this.newSteamApps + " games";
      this.alert.Text = "Scanning ready";
      this.alert.labelMessage.Text = str;
      this.alert.progressBar1.Visible = false;
      this.alert.buttonCancel.Visible = false;
      this.alert.okButton.Visible = true;
      this.EnableButton(true);
      this.alert.Close();
    }

    private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      string str = e.ProgressPercentage.ToString() + "%";
      Console.WriteLine(e.ProgressPercentage);
      this.alert.Message = "In progress, please wait... " + e.ProgressPercentage.ToString() + "%";
      this.alert.ProgressValue = e.ProgressPercentage;
    }

    private bool AddBlizzardGame(string gameTitle)
    {
      string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" 
                + gameTitle;

      string file = (string) Registry.GetValue(keyName, "DisplayIcon", (object) null);
      if (file != null)
      {
        Icon icon1 = Icon.FromHandle(new Bitmap(16, 16).GetHicon());
        string appName = (string) Registry.GetValue(keyName, "DisplayName", (object) null);
        string str = (string) Registry.GetValue(keyName, "InstallLocation", (object) null);
        if (icon1.Width < 256)
        {
          foreach (Icon icon2 in IconHelper.SplitGroupIcon(IconHelper.ExtractIcon(file, 0)))
          {
            if (icon2.Size.Width > icon1.Width)
              icon1 = icon2;
          }
        }
        this.iconList.Images.Add(icon1);
        this.largeIconList.Images.Add(icon1);
        int iconListIndex = this.iconList.Images.Count - 1;
        if (gameTitle.Equals("Hearthstone"))
          file = Directory.GetFiles(str, "*Launcher*.exe")[0];
        Form1.AppInfo appInfo = new Form1.AppInfo(appName, file, str, file, true, iconListIndex, false);
        bool flag = false;
        for (int index = 0; index < this.apps.Count; ++index)
        {
          if (this.apps[index].appPath.Equals(appInfo.appPath))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          this.apps.Add(appInfo);
          return true;
        }
      }
      return false;
    }

    private int AddBlizzardGames()
    {
      int num = 0;
      if (this.AddBlizzardGame("Hearthstone"))
        ++num;
      if (this.AddBlizzardGame("Diablo III"))
        ++num;
      if (this.AddBlizzardGame("World of Warcraft"))
        ++num;
      return num;
    }

    private int AddOriginGames(string[] steampaths)
    {
      string name = "Software";
      RegistryKey parentKey = Registry.LocalMachine.OpenSubKey(name);
      int num = 0;
      using (RegistryKey registryKey1 = Form1._openHKLMSubKey(parentKey,
          "Software\\Microsoft\\Windows\\CurrentVersion\\GameUX\\Games"))
      {
        foreach (string subKeyName1 in registryKey1.GetSubKeyNames())
        {
          bool flag1 = false;

          string subKeyName2 = "Software\\Microsoft\\Windows\\CurrentVersion\\GameUX\\Games\\" 
                        + subKeyName1;
          RegistryKey registryKey2 = Form1._openHKLMSubKey(parentKey, subKeyName2);
          string str = Convert.ToString(registryKey2.GetValue("AppExePath"));
          string b = Convert.ToString(registryKey2.GetValue("ConfigApplicationPath"));
          foreach (string steampath in steampaths)
          {
            if (!string.IsNullOrEmpty(steampath) && string.Equals(steampath, b, 
                StringComparison.CurrentCultureIgnoreCase))
            {
              Console.WriteLine(steampath);
              flag1 = true;
            }
          }
          if (!flag1 && !string.IsNullOrEmpty(str))
          {
            Icon icon1 = Icon.FromHandle(new Bitmap(16, 16).GetHicon());
            string appName = Convert.ToString(registryKey2.GetValue("Title"));
            string folder = str;
            if (icon1.Width < 256)
            {
              foreach (Icon icon2 in IconHelper.SplitGroupIcon(IconHelper.ExtractIcon(str, 0)))
              {
                if (icon2.Size.Width > icon1.Width)
                  icon1 = icon2;
              }
            }
            this.iconList.Images.Add(icon1);
            this.largeIconList.Images.Add(icon1);
            int iconListIndex = this.iconList.Images.Count - 1;
            Form1.AppInfo appInfo = new Form1.AppInfo(appName, str, folder, str, true, iconListIndex, false);
            bool flag2 = false;
            for (int index = 0; index < this.apps.Count; ++index)
            {
              if (this.apps[index].appPath.Equals(appInfo.appPath))
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
            {
              Console.WriteLine(appName);
              this.apps.Add(appInfo);
              ++num;
            }
          }
        }
      }
      return num;
    }

    private static RegistryKey _openHKLMSubKey(RegistryKey parentKey, string subKeyName)
    {
      int phkResult;
      return Form1.RegOpenKeyEx((UIntPtr) 2147483650U, subKeyName, 0, 131353, out phkResult) != 0 
                ? (RegistryKey) null : Form1._pointerToRegistryKey((IntPtr) phkResult, false, false);
    }

    private static RegistryKey _pointerToRegistryKey(IntPtr hKey, bool writable, bool ownsHandle)
    {
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
      System.Type type1 = typeof (SafeHandleZeroOrMinusOneIsInvalid).Assembly.GetType(
          "Microsoft.Win32.SafeHandles.SafeRegistryHandle");
      System.Type[] types1 = new System.Type[2]
      {
        typeof (IntPtr),
        typeof (bool)
      };

      object obj = type1.GetConstructor(bindingAttr, (Binder) null, types1, 
          (ParameterModifier[]) null).Invoke(new object[2]
      {
        (object) hKey,
        (object) ownsHandle
      });
      System.Type type2 = typeof (RegistryKey);
      System.Type[] types2 = new System.Type[2]
      {
        type1,
        typeof (bool)
      };
      return (RegistryKey) type2.GetConstructor(bindingAttr, (Binder) null, types2,
          (ParameterModifier[]) null).Invoke(new object[2]
      {
        obj,
        (object) writable
      });
    }

    [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
    public static extern int RegOpenKeyEx(
      UIntPtr hKey,
      string subKey,
      int ulOptions,
      int samDesired,
      out int phkResult);

    [DllImport("advapi32.dll", EntryPoint = "RegQueryValueEx")]
    public static extern int RegQueryValueEx_DllImport(
      IntPtr hKey,
      string lpValueName,
      int lpReserved,
      out uint lpType,
      StringBuilder lpData,
      ref uint lpcbData);

    [DllImport("advapi32.dll", EntryPoint = "RegOpenKeyEx")]
    public static extern int RegOpenKeyEx_DllImport(
      UIntPtr hKey,
      string subKey,
      uint options,
      int sam,
      out IntPtr phkResult);

    private void applistBw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.linkLabel1.Enabled = true;
      this.steamScanButton.Enabled = true;
      this.browseButton.Enabled = true;
      this.objectListView1.EmptyListMsg = "Add applications with 'Browse..' button";
      this.apps.Sort();
      this.objectListView1.RefreshObjects((IList) this.apps);
    }

    private void applistBw_DoWork(object sender, DoWorkEventArgs e) => this.LoadAppConfig();

    private void ipbw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (this.externalIP.Length > 10)
        this.externalIPlabel.Text = this.externalIP;
      else
        this.externalIPlabel.Text = "N/A";
      this.updateAppButton.Visible = true;
      //if (Form1.UpdateAvailable(this.recentVersion) == 0)
        return;
      //this.updateAppButton.Enabled = true;
      //this.updateAppButton.Text = "Update available";
    }

    private void ipbw_DoWork(object sender, DoWorkEventArgs e)
    {
      try
      {
        this.externalIP = new WebClient().DownloadString("http://checkip.dyndns.org/");
      }
      catch
      {
        this.externalIP = "NOT AVAILABLE";
        return;
      }
      this.externalIP = new Regex("\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}").Matches(this.externalIP)[0].ToString();
      byte[] vvv = default;

      try
      {
        vvv = new WebClient().DownloadData("http://www.kinoni.com/remote/version.txt");
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] WebClient().DownloadData error: " + ex.Message);
      }
      int ver = 1;
      if (vvv != null)
        ver = Convert.ToInt32(
          Encoding.Default.GetString(vvv).Split(':')[0]);
      this.recentVersion = ver;
    }

    private void bw_DoWork(object sender, DoWorkEventArgs e)
    {
      BackgroundWorker backgroundWorker = sender as BackgroundWorker;
      int num1 = 0;
      int num2 = this.AddBlizzardGames();
      string name = "SOFTWARE\\Valve\\Steam\\Apps";
      string[] steampaths;
      using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name))
      {
        string[] subKeyNames = registryKey.GetSubKeyNames();
        int length = subKeyNames.Length;
        steampaths = new string[length];
        int index1 = 0;
        foreach (string str1 in subKeyNames)
        {
          if (backgroundWorker.CancellationPending)
          {
            e.Cancel = true;
            break;
          }
          string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App " + str1;
          string appName = (string) Registry.GetValue(keyName, "DisplayName", (object) null);
          steampaths[index1] = (string) Registry.GetValue(keyName, "InstallLocation", (object) null);
          string appPath = "steam://rungameid/" + str1;
          string str2 = (string) Registry.GetValue(keyName, "InstallLocation", (object) null);
          if (Directory.Exists(str2))
          {
            Icon bestIcon = Icon.FromHandle(new Bitmap(16, 16).GetHicon());
            string empty = string.Empty;
            this.DirSearch(str2, ref bestIcon, ref empty);
            this.iconList.Images.Add(bestIcon);
            this.largeIconList.Images.Add(bestIcon);
            int iconListIndex = this.iconList.Images.Count - 1;
            Form1.AppInfo appInfo = new Form1.AppInfo(appName, appPath, str2, empty, true, iconListIndex, true);
            bool flag = false;
            for (int index2 = 0; index2 < this.apps.Count; ++index2)
            {
              if (this.apps[index2].appPath.Equals(appInfo.appPath))
                flag = true;
            }
            if (!flag)
            {
              this.apps.Add(appInfo);
              ++num2;
            }
            ++index1;
            int percentProgress = (int) ((double) index1 * 100.0 / (double) length);
            backgroundWorker.ReportProgress(percentProgress);
          }
        }
      }
      this.AddOriginGames(steampaths);
      this.newSteamApps = num2;
      Icon icon1 = (Icon) null;

      string str = (string) Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam", 
          "SteamExe", (object) null);
      if (str != null)
      {
        foreach (Icon icon2 in IconHelper.SplitGroupIcon(IconHelper.ExtractIcon(str, 0)))
        {
          if (icon1 == null || icon2.Size.Width > icon1.Width)
            icon1 = icon2;
        }
        this.iconList.Images.Add(icon1);
        this.largeIconList.Images.Add(icon1);
        int iconListIndex = this.iconList.Images.Count - 1;

        Form1.AppInfo appInfo = new Form1.AppInfo("Steam Big Picture Mode", 
            "steam://open/bigpicture", str, str, true, iconListIndex, false);
        bool flag = false;
        for (int index = 0; index < this.apps.Count; ++index)
        {
          if (this.apps[index].appPath.Equals(appInfo.appPath))
            flag = true;
        }
        if (!flag)
        {
          this.apps.Insert(0, appInfo);
          num1 = num2 + 1;
        }
      }
      this.apps.Sort();
      this.objectListView1.SetObjects((IEnumerable) this.apps);
    }

    private void DirSearch(string sDir, ref Icon bestIcon, ref string iconPath)
    {
      try
      {
        foreach (string file in Directory.GetFiles(sDir, "*.exe"))
        {
          try
          {
            if (bestIcon.Width < 256)
            {
              foreach (Icon icon in IconHelper.SplitGroupIcon(IconHelper.ExtractIcon(file, 0)))
              {
                if (icon.Size.Width > bestIcon.Width)
                {
                  bestIcon = icon;
                  iconPath = file;
                }
              }
              if (bestIcon.Width == 256)
              {
                if (bestIcon.Height == 256)
                  break;
              }
            }
          }
          catch (Exception ex)
          {
          }
        }
        foreach (string directory in Directory.GetDirectories(sDir))
        {
          if (bestIcon.Width < 256)
            this.DirSearch(directory, ref bestIcon, ref iconPath);
        }
      }
      catch (Exception ex)
      {
      }
    }

    private static string scanKey(string contents, string keyname)
    {
      int num1 = contents.IndexOf(keyname, StringComparison.OrdinalIgnoreCase) + keyname.Length;
      int num2 = contents.IndexOf('"', num1 + 1);
      int num3 = contents.IndexOf('"', num2 + 1);
      return contents.Substring(num2 + 1, num3 - num2 - 1);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.OKbutton = new Button();
      this.cancelButton = new Button();
      this.tabControl1 = new TabControl();
      this.tabPage2 = new TabPage();
      this.groupBox4 = new GroupBox();
      this.googleLoginButton = new Button();
      this.googleStatusLabel = new Label();
      this.label7 = new Label();
      this.googlePasswordTextBox = new TextBox();
      this.label6 = new Label();
      this.googleUsernameTextBox = new TextBox();
      this.label5 = new Label();
      this.groupBox3 = new GroupBox();
      this.externalIPlabel = new Label();
      this.label9 = new Label();
      this.computerNameLabel = new Label();
      this.label8 = new Label();
      this.groupBox2 = new GroupBox();
      this.pwdStatusPictureBox = new PictureBox();
      this.retypePasswordTextBox = new TextBox();
      this.passwordTextBox = new TextBox();
      this.label4 = new Label();
      this.label3 = new Label();
      this.label2 = new Label();
      this.groupBox1 = new GroupBox();
      this.pictureBox1 = new PictureBox();
      this.updateAppButton = new Button();
      this.label11 = new Label();
      this.label1 = new Label();
      this.tabPage1 = new TabPage();
      this.linkLabel1 = new LinkLabel();
      this.steamScanButton = new Button();
      this.deleteButton = new Button();
      this.browseButton = new Button();
      this.tabPage3 = new TabPage();
      this.groupBox6 = new GroupBox();
      this.label10 = new Label();
      this.XBoxCombo = new ComboBox();
      this.muteBox = new CheckBox();
      this.groupBox5 = new GroupBox();
      this.audioDeviceCombo = new ComboBox();
      this.objectListView1 = new ObjectListView();
      this.iconColumn = new OLVColumn();
      this.nameColumn = new OLVColumn();
      this.launchCmdColumn = new OLVColumn();
      this.enabledColumn = new OLVColumn();
      this.tabControl1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((ISupportInitialize) this.pwdStatusPictureBox).BeginInit();
      this.groupBox1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.tabPage1.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.groupBox5.SuspendLayout();
      ((ISupportInitialize) this.objectListView1).BeginInit();
      this.SuspendLayout();
      this.OKbutton.Font = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.OKbutton.Location = new Point(409, 511);
      this.OKbutton.Name = "OKbutton";
      this.OKbutton.Size = new Size(173, 41);
      this.OKbutton.TabIndex = 1;
      this.OKbutton.Text = "OK";
      this.OKbutton.UseVisualStyleBackColor = true;
      this.OKbutton.Click += new EventHandler(this.OKbutton_Click);
      this.cancelButton.Font = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cancelButton.Location = new Point(604, 511);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new Size(173, 41);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.CancelButton_Click);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage3);
      this.tabControl1.Font = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.tabControl1.Location = new Point(12, 13);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(770, 492);
      this.tabControl1.TabIndex = 3;
      this.tabPage2.Controls.Add((Control) this.groupBox4);
      this.tabPage2.Controls.Add((Control) this.groupBox3);
      this.tabPage2.Controls.Add((Control) this.groupBox2);
      this.tabPage2.Controls.Add((Control) this.groupBox1);
      this.tabPage2.Location = new Point(4, 26);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(762, 462);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Connection";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.groupBox4.Controls.Add((Control) this.googleLoginButton);
      this.groupBox4.Controls.Add((Control) this.googleStatusLabel);
      this.groupBox4.Controls.Add((Control) this.label7);
      this.groupBox4.Controls.Add((Control) this.googlePasswordTextBox);
      this.groupBox4.Controls.Add((Control) this.label6);
      this.groupBox4.Controls.Add((Control) this.googleUsernameTextBox);
      this.groupBox4.Controls.Add((Control) this.label5);
      this.groupBox4.Location = new Point(7, 322);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(754, 134);
      this.groupBox4.TabIndex = 4;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Google authentication";
      this.googleLoginButton.Location = new Point(618, 86);
      this.googleLoginButton.Name = "googleLoginButton";
      this.googleLoginButton.Size = new Size(104, 31);
      this.googleLoginButton.TabIndex = 10;
      this.googleLoginButton.Text = "Log in";
      this.googleLoginButton.UseVisualStyleBackColor = true;
      this.googleLoginButton.Click += new EventHandler(this.googleLoginButton_Click);
      this.googleStatusLabel.AutoSize = true;
      this.googleStatusLabel.Location = new Point(614, 54);

      this.googleStatusLabel.Name = "googleStatusLabel";
      this.googleStatusLabel.Size = new Size(112, 19);
      this.googleStatusLabel.TabIndex = 9;
      this.googleStatusLabel.Text = "status: logged out";
      
      this.label7.AutoSize = true;
      this.label7.Location = new Point(47, 20);
      this.label7.Name = "label7";
      this.label7.Size = new Size(642, 19);
      this.label7.TabIndex = 8;

      this.label7.Text = "Servers can also be found via Google account. " +
                "Use the same Google id in both the server and the client.";

      this.googlePasswordTextBox.Location = new Point(313, 99);
      this.googlePasswordTextBox.Name = "googlePasswordTextBox";
      this.googlePasswordTextBox.PasswordChar = '•';
      this.googlePasswordTextBox.Size = new Size(239, 25);
      this.googlePasswordTextBox.TabIndex = 7;
      this.label6.AutoSize = true;
      this.label6.Location = new Point(206, 102);
      this.label6.Name = "label6";
      this.label6.Size = new Size(67, 19);
      this.label6.TabIndex = 6;
      this.label6.Text = "Password";
      this.googleUsernameTextBox.Location = new Point(313, 63);
      this.googleUsernameTextBox.Name = "googleUsernameTextBox";
      this.googleUsernameTextBox.Size = new Size(239, 25);
      this.googleUsernameTextBox.TabIndex = 5;
      this.label5.AutoSize = true;
      this.label5.Location = new Point(206, 66);
      this.label5.Name = "label5";
      this.label5.Size = new Size(71, 19);
      this.label5.TabIndex = 5;
      this.label5.Text = "Username";
      this.groupBox3.Controls.Add((Control) this.externalIPlabel);
      this.groupBox3.Controls.Add((Control) this.label9);
      this.groupBox3.Controls.Add((Control) this.computerNameLabel);
      this.groupBox3.Controls.Add((Control) this.label8);
      this.groupBox3.Location = new Point(7, 243);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(754, 77);
      this.groupBox3.TabIndex = 3;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Server information";
      this.externalIPlabel.AutoSize = true;
      this.externalIPlabel.Font = new Font("Segoe UI", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.externalIPlabel.ForeColor = System.Drawing.Color.MediumSeaGreen;
      this.externalIPlabel.Location = new Point(309, 47);
      this.externalIPlabel.Name = "externalIPlabel";
      this.externalIPlabel.Size = new Size(36, 19);
      this.externalIPlabel.TabIndex = 3;
      this.externalIPlabel.Text = "N/A";
      this.label9.AutoSize = true;
      this.label9.Location = new Point(148, 47);
      this.label9.Name = "label9";
      this.label9.Size = new Size((int) sbyte.MaxValue, 19);
      this.label9.TabIndex = 2;
      this.label9.Text = "External IP address:";
      this.computerNameLabel.AutoSize = true;
      this.computerNameLabel.Font = new Font("Segoe UI", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.computerNameLabel.ForeColor = System.Drawing.Color.MediumSeaGreen;
      this.computerNameLabel.Location = new Point(309, 25);
      this.computerNameLabel.Name = "computerNameLabel";
      this.computerNameLabel.Size = new Size(143, 19);
      this.computerNameLabel.TabIndex = 1;
      this.computerNameLabel.Text = "Computernamehere";
      this.label8.AutoSize = true;
      this.label8.Location = new Point(190, 25);
      this.label8.Name = "label8";
      this.label8.Size = new Size(88, 19);
      this.label8.TabIndex = 0;
      this.label8.Text = "Server name:";
      this.groupBox2.Controls.Add((Control) this.pwdStatusPictureBox);
      this.groupBox2.Controls.Add((Control) this.retypePasswordTextBox);
      this.groupBox2.Controls.Add((Control) this.passwordTextBox);
      this.groupBox2.Controls.Add((Control) this.label4);
      this.groupBox2.Controls.Add((Control) this.label3);
      this.groupBox2.Controls.Add((Control) this.label2);
      this.groupBox2.Location = new Point(7, 91);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(754, 150);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Security";
      this.pwdStatusPictureBox.Location = new Point(567, 73);
      this.pwdStatusPictureBox.Name = "pwdStatusPictureBox";
      this.pwdStatusPictureBox.Size = new Size(60, 61);
      this.pwdStatusPictureBox.TabIndex = 5;
      this.pwdStatusPictureBox.TabStop = false;
      this.retypePasswordTextBox.Location = new Point(313, 109);
      this.retypePasswordTextBox.MaxLength = 32;
      this.retypePasswordTextBox.Name = "retypePasswordTextBox";
      this.retypePasswordTextBox.PasswordChar = '•';
      this.retypePasswordTextBox.Size = new Size(239, 25);
      this.retypePasswordTextBox.TabIndex = 4;
      this.retypePasswordTextBox.TextChanged += new EventHandler(this.repasswordText_Changed);
      this.passwordTextBox.Location = new Point(313, 73);
      this.passwordTextBox.MaxLength = 32;
      this.passwordTextBox.Name = "passwordTextBox";
      this.passwordTextBox.PasswordChar = '•';
      this.passwordTextBox.Size = new Size(239, 25);
      this.passwordTextBox.TabIndex = 3;
      this.passwordTextBox.TextChanged += new EventHandler(this.passwordText_Changed);
      this.label4.AutoSize = true;
      this.label4.Location = new Point(148, 109);
      this.label4.Name = "label4";
      this.label4.Size = new Size(119, 19);
      this.label4.TabIndex = 2;
      this.label4.Text = "Re-type password";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(148, 73);
      this.label3.Name = "label3";
      this.label3.Size = new Size(67, 19);
      this.label3.TabIndex = 1;
      this.label3.Text = "Password";
      this.label2.Location = new Point(42, 25);
      this.label2.Name = "label2";
      this.label2.Size = new Size(687, 64);
      this.label2.TabIndex = 0;

      this.label2.Text = "Set password to protect your computer from unauthorized access. " +
                "It is recommended to use at least 8 alphanumeric characters. " +
                "Maximum password length is 32 characters.";
      
      this.groupBox1.Controls.Add((Control) this.pictureBox1);
      this.groupBox1.Controls.Add((Control) this.updateAppButton);
      this.groupBox1.Controls.Add((Control) this.label11);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Location = new Point(6, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(755, 83);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.pictureBox1.BackgroundImage = (Image) componentResourceManager.GetObject(
          "pictureBox1.BackgroundImage");
      this.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
      this.pictureBox1.Location = new Point(169, 15);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(68, 65);
      this.pictureBox1.TabIndex = 3;
      this.pictureBox1.TabStop = false;

      this.updateAppButton.Enabled = false;
      
      this.updateAppButton.Location = new Point(536, 28);
      this.updateAppButton.Name = "updateAppButton";
      //this.updateAppButton.UseVisualStyleBackColor = true;
      this.updateAppButton.Size = new Size(/*194*/0, /*35*/0); // HIDE THIS BUTTON (NOT NEEDED)       
      this.updateAppButton.TabIndex = 2;
      this.updateAppButton.Text = /*"Application is up to date"*/"";
      
      this.updateAppButton.Click += new EventHandler(this.updateAppButton_Click);
      
      this.label11.AutoSize = true;
      this.label11.Location = new Point(332, 23);
      this.label11.Name = "label11";
      this.label11.Size = new Size(129, 19);
      this.label11.TabIndex = 1;
      this.label11.Text = /*"Version 1.34"*/"Version 2.00-alpha";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(332, 51);
      this.label1.Name = "label1";
      this.label1.Size = new Size(169, 19);
      this.label1.TabIndex = 0;
      this.label1.Text = /*"Copyright (C) 2015 Kinoni"*/ "Kinoni Remote Desktop Config :: 2024";
      this.tabPage1.Controls.Add((Control) this.linkLabel1);
      this.tabPage1.Controls.Add((Control) this.steamScanButton);
      this.tabPage1.Controls.Add((Control) this.deleteButton);
      this.tabPage1.Controls.Add((Control) this.browseButton);
      this.tabPage1.Controls.Add((Control) this.objectListView1);
      this.tabPage1.Location = new Point(4, 26);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(762, 462);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Quick Launch";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Enabled = false;
      this.linkLabel1.LinkColor = System.Drawing.Color.Red;
      this.linkLabel1.Location = new Point(6, 406);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(214, 19);
      this.linkLabel1.TabIndex = 4;
      ((Label) this.linkLabel1).TabStop = true;
      this.linkLabel1.Text = "Install Xbox 360 controller drivers";
      this.steamScanButton.Enabled = false;
      this.steamScanButton.Location = new Point((int) byte.MaxValue, 412);
      this.steamScanButton.Name = "steamScanButton";
      this.steamScanButton.Size = new Size(160, 40);
      this.steamScanButton.TabIndex = 3;
      this.steamScanButton.Text = "Scan Installed Games";
      this.steamScanButton.UseVisualStyleBackColor = true;
      this.steamScanButton.Click += new EventHandler(this.steamScanButton_Click);
      this.deleteButton.Enabled = false;
      this.deleteButton.Location = new Point(424, 412);
      this.deleteButton.Name = "deleteButton";
      this.deleteButton.Size = new Size(160, 40);
      this.deleteButton.TabIndex = 2;
      this.deleteButton.Text = "Delete";
      this.deleteButton.UseVisualStyleBackColor = true;
      this.deleteButton.Click += new EventHandler(this.deleteButton_Click);
      this.browseButton.Enabled = false;
      this.browseButton.Location = new Point(591, 412);
      this.browseButton.Name = "browseButton";
      this.browseButton.Size = new Size(160, 40);
      this.browseButton.TabIndex = 1;
      this.browseButton.Text = "Browse..";
      this.browseButton.UseVisualStyleBackColor = true;
      this.browseButton.Click += new EventHandler(this.AddButton_Click);
      this.tabPage3.Controls.Add((Control) this.groupBox6);
      this.tabPage3.Controls.Add((Control) this.muteBox);
      this.tabPage3.Controls.Add((Control) this.groupBox5);
      this.tabPage3.Location = new Point(4, 26);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new Size(762, 462);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Advanced";
      this.tabPage3.UseVisualStyleBackColor = true;
      this.groupBox6.Controls.Add((Control) this.label10);
      this.groupBox6.Controls.Add((Control) this.XBoxCombo);
      this.groupBox6.Location = new Point(3, 111);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new Size(752, 144);
      this.groupBox6.TabIndex = 2;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Xbox controller emulation mode";
      this.label10.Location = new Point(16, 21);
      this.label10.Name = "label10";
      this.label10.Size = new Size(721, 64);
      this.label10.TabIndex = 3;
      this.label10.Text = componentResourceManager.GetString("label10.Text");
      this.XBoxCombo.DropDownStyle = ComboBoxStyle.DropDownList;
      this.XBoxCombo.FormattingEnabled = true;
      this.XBoxCombo.Items.AddRange(new object[3]
      {
        (object) "Xbox controller emulation enabled",
        (object) "Xbox controller emulation enabled while client connected",
        (object) "Xbox controller emulation disabled"
      });
      this.XBoxCombo.Location = new Point(6, 96);
      this.XBoxCombo.Name = "XBoxCombo";
      this.XBoxCombo.Size = new Size(394, 25);
      this.XBoxCombo.TabIndex = 5;
      this.muteBox.AutoSize = true;
      this.muteBox.Location = new Point(9, 70);
      this.muteBox.Name = "muteBox";
      this.muteBox.Size = new Size(258, 23);
      this.muteBox.TabIndex = 2;
      this.muteBox.Text = "Mute PC audio while client connected";
      this.muteBox.UseVisualStyleBackColor = true;
      this.groupBox5.Controls.Add((Control) this.audioDeviceCombo);
      this.groupBox5.Location = new Point(3, 3);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new Size(752, 60);
      this.groupBox5.TabIndex = 1;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Audio Device";
      this.audioDeviceCombo.DropDownStyle = ComboBoxStyle.DropDownList;
      this.audioDeviceCombo.FormattingEnabled = true;
      this.audioDeviceCombo.Location = new Point(6, 24);
      this.audioDeviceCombo.Name = "audioDeviceCombo";
      this.audioDeviceCombo.Size = new Size(394, 25);
      this.audioDeviceCombo.TabIndex = 0;
      this.objectListView1.AllowDrop = true;
      this.objectListView1.Columns.AddRange(new ColumnHeader[4]
      {
        (ColumnHeader) this.iconColumn,
        (ColumnHeader) this.nameColumn,
        (ColumnHeader) this.launchCmdColumn,
        (ColumnHeader) this.enabledColumn
      });
      this.objectListView1.IsSimpleDropSink = true;
      this.objectListView1.Location = new Point(3, 3);
      this.objectListView1.Name = "objectListView1";
      this.objectListView1.Size = new Size(760, 400);
      this.objectListView1.TabIndex = 0;
      this.objectListView1.UseCellFormatEvents = true;
      this.objectListView1.UseCompatibleStateImageBehavior = false;
      this.objectListView1.View = View.Details;
      this.objectListView1.FormatCell += new EventHandler<FormatCellEventArgs>(
          this.objectListView1_FormatCell);
      this.iconColumn.AspectName = "";
      this.iconColumn.CellPadding = new Rectangle?();
      this.iconColumn.MaximumWidth = 48;
      this.iconColumn.MinimumWidth = 48;
      this.iconColumn.Text = "Icon";
      this.iconColumn.Width = 48;
      this.iconColumn.ImageGetter = new ImageGetterDelegate(this.GameImageGetter);
      this.nameColumn.AspectName = "appName";
      this.nameColumn.CellPadding = new Rectangle?();
      this.nameColumn.MaximumWidth = 300;
      this.nameColumn.MinimumWidth = 300;
      this.nameColumn.Text = "Name";
      this.nameColumn.Width = 300;
      this.launchCmdColumn.AspectName = "appPath";
      this.launchCmdColumn.CellPadding = new Rectangle?();
      this.launchCmdColumn.MaximumWidth = 300;
      this.launchCmdColumn.MinimumWidth = 300;
      this.launchCmdColumn.Text = "Launch command";
      this.launchCmdColumn.Width = 300;
      this.enabledColumn.AspectName = "appEnabled";
      this.enabledColumn.CellPadding = new Rectangle?();
      this.enabledColumn.HeaderTextAlign = HorizontalAlignment.Center;
      this.enabledColumn.MaximumWidth = 85;
      this.enabledColumn.MinimumWidth = 85;
      this.enabledColumn.Text = "Enabled";
      this.enabledColumn.TextAlign = HorizontalAlignment.Center;
      this.enabledColumn.Width = 85;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(784, 561);
      this.Controls.Add((Control) this.tabControl1);
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.OKbutton);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (Form1);
      this.Text = "Kinoni Remote Desktop";
      this.tabControl1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((ISupportInitialize) this.pwdStatusPictureBox).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      ((ISupportInitialize) this.objectListView1).EndInit();
      this.ResumeLayout(false);
    }

    public class AppInfo : IComparable
    {
      public string appName;
      public string appPath;
      public string appIconPath;
      public bool appEnabled;
      public int iconListIndex;
      public bool steamApp;
      public string folder;

      public AppInfo(
        string appName,
        string appPath,
        string folder,
        string appIconPath,
        bool appEnabled,
        int iconListIndex,
        bool steamApp)
      {
        this.appName = appName;
        this.appPath = appPath;
        this.folder = folder;
        this.appIconPath = appIconPath;
        this.appEnabled = appEnabled;
        this.iconListIndex = iconListIndex;
        this.steamApp = steamApp;
      }

      public int CompareTo(object obj)
      {
        if (!(obj is Form1.AppInfo appInfo))
          return 1;
        if (this.appPath.Equals("steam://open/bigpicture"))
          return -1;
        if (appInfo.appPath.Equals("steam://open/bigpicture"))
          return 1;
        return appInfo == this ? 0 : this.appName.CompareTo(appInfo.appName);
      }
    }

    private delegate void EnableButtonCallback(bool enabled);

    private enum RegWow64Options
    {
      None = 0,
      KEY_WOW64_64KEY = 256, // 0x00000100
      KEY_WOW64_32KEY = 512, // 0x00000200
    }

    private enum RegistryRights
    {
      WriteKey = 131078, // 0x00020006
      ReadKey = 131097, // 0x00020019
    }
  }
}
