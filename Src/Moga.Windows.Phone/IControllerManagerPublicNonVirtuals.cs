// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.__IControllerManagerPublicNonVirtuals
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using GuidAttribute = Windows.Foundation.Metadata.GuidAttribute;

namespace Moga.Windows.Phone
{
  [Guid(2541015034, 8037, 16299, 136, 246, 0, 36, 80, 44, 38, 81)]
  [Version(1)]
  [ExclusiveTo(typeof (ControllerManager))]
  internal interface IControllerManagerPublicNonVirtuals
  {
    void Connect();

    ControllerAction GetKeyCode([In] KeyCode keyCode);

    float GetAxisValue([In] Axis axis);

    ControllerResult GetState([In] ControllerState info);

    void SetPacketMonitor([In] PacketMonitorDelegate pmd);

    [Overload("SetListener1")]
    void SetListener([In] IControllerListener listener);

    void Suspending();

    void Resuming();

    void Close();

    void OpenBluetoothSettings();

    event KeyEventHandler KeyChanged;

    event MotionEventHandler AxisChanged;

    event StateEventHandler StateChanged;

    event ConnectionRetryHandler ConnectionRetry;

    event ConnectHandler Connected;

    event DisconnectHandler Disconnected;

    event BluetoothUnavailableHandler BluetoothUnavailable;

    bool EventsActive { get; }
  }
}
