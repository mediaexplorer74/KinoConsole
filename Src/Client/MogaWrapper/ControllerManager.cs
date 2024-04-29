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
  public sealed class ControllerManager : /*IControllerManagerPublicNonVirtuals,*/ IControllerManagerProtectedNonVirtuals
  {
    //[Overload("CreateInstance1")]
    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "CreateInstance1")]
    public static extern void CreateInstance1([MarshalAs(UnmanagedType.Interface)] out IControllerManagerPublicNonVirtuals output);

    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    //[DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "ControllerManager")]
    static extern ControllerManager();

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Connnect")]
    public static extern void Connect();

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetKeyCode")]
    public static extern ControllerAction GetKeyCode([In] KeyCode keyCode);

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetAxisValue")]
    public static extern float GetAxisValue([In] Axis axis);

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetState")]
    public static extern ControllerResult GetState([In] ControllerState info);

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "SetPacketMoniker")]
    public static extern void SetPacketMonitor([In] PacketMonitorDelegate pmd);

    [Overload("SetListener1")]

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "SetListener")]
    public static extern void SetListener([In] IControllerListener listener);

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Suspending")]
    public static extern void Suspending();

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Resuming")]
    public static extern void Resuming();

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Close")]
    public static extern void Close();

    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "OpenBluetoothSettings")]
    public static extern void OpenBluetoothSettings();

    public static extern event KeyEventHandler KeyChanged;

    public extern event MotionEventHandler AxisChanged;

    public extern event StateEventHandler StateChanged;

    public extern event ConnectionRetryHandler ConnectionRetry;

    public extern event ConnectHandler Connected;

    public extern event DisconnectHandler Disconnected;

    public extern event BluetoothUnavailableHandler BluetoothUnavailable;

    public extern bool EventsActive
    { 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            get; 
    }
  }
}
