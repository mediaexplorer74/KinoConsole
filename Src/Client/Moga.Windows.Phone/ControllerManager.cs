// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.ControllerManager
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace Moga.Windows.Phone
{
  //[MarshalingBehavior]
  //[Threading]
  [Activatable(1)]
  [Version(1)]
  public sealed class ControllerManager : 
    IControllerManagerPublicNonVirtuals,
    IControllerManagerProtectedNonVirtuals
  {
    //[Overload("CreateInstance1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern ControllerManager();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void Connect();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern ControllerAction GetKeyCode([In] KeyCode keyCode);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern float GetAxisValue([In] Axis axis);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern ControllerResult GetState([In] ControllerState info);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void SetPacketMonitor([In] PacketMonitorDelegate pmd);

    [Overload("SetListener1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void SetListener([In] IControllerListener listener);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void Suspending();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void Resuming();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void Close();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void OpenBluetoothSettings();

    public extern event KeyEventHandler KeyChanged;

    public extern event MotionEventHandler AxisChanged;

    public extern event StateEventHandler StateChanged;

    public extern event ConnectionRetryHandler ConnectionRetry;

    public extern event ConnectHandler Connected;

    public extern event DisconnectHandler Disconnected;

    public extern event BluetoothUnavailableHandler BluetoothUnavailable;

    public extern bool EventsActive { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }
  }
}
