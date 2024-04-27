// Decompiled with JetBrains decompiler
// Type: ConfigDialogForm.Properties.Resources
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace ConfigDialogForm.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [CompilerGenerated]
  [DebuggerNonUserCode]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) ConfigDialogForm.Properties.Resources.resourceMan, (object) null))
          ConfigDialogForm.Properties.Resources.resourceMan = new ResourceManager("ConfigDialogForm.Properties.Resources", typeof (ConfigDialogForm.Properties.Resources).Assembly);
        return ConfigDialogForm.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => ConfigDialogForm.Properties.Resources.resourceCulture;
      set => ConfigDialogForm.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap button_nok
    {
      get
      {
        return (Bitmap) ConfigDialogForm.Properties.Resources.ResourceManager.GetObject(nameof (button_nok), ConfigDialogForm.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap button_ok
    {
      get
      {
        return (Bitmap) ConfigDialogForm.Properties.Resources.ResourceManager.GetObject(nameof (button_ok), ConfigDialogForm.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap consoleicon512
    {
      get
      {
        return (Bitmap) ConfigDialogForm.Properties.Resources.ResourceManager.GetObject(nameof (consoleicon512), ConfigDialogForm.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap remoteicon512x512
    {
      get
      {
        return (Bitmap) ConfigDialogForm.Properties.Resources.ResourceManager.GetObject(nameof (remoteicon512x512), ConfigDialogForm.Properties.Resources.resourceCulture);
      }
    }

    internal static Icon remove_256
    {
      get
      {
        return (Icon) ConfigDialogForm.Properties.Resources.ResourceManager.GetObject(nameof (remove_256), ConfigDialogForm.Properties.Resources.resourceCulture);
      }
    }
  }
}
