// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.ControllerResult
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using Windows.Foundation.Metadata;

namespace Moga.Windows.Phone
{
  //[Version(1)]
  public enum ControllerResult
  {
    Connected = 0,
    True = 0,
    Disconnected = 1,
    False = 1,
    Connecting = 2,
    NoMogaDevicePairingFound = 3,
    VersionMoga = 20, // 0x00000014
    VersionMogaPro = 21, // 0x00000015
  }
}
