// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.StateEvent
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace Moga.Windows.Phone
{
  //[Threading]
  [Activatable(typeof (IStateEventFactory), 1)]
  [Version(1)]
  //[MarshalingBehavior]
  public sealed class StateEvent : IStateEventPublicNonVirtuals
  {
    //[Overload("CreateInstance1")]
    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "CreateInstance1")]
    static extern void CreateInstance1([MarshalAs(UnmanagedType.Interface)] out IControllerManagerPublicNonVirtuals output);
    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern StateEvent([In] ControllerState stateKey, [In] ControllerResult stateValue);

    public extern ControllerState StateKey 
    { 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            get; 
    }

    public extern ControllerResult StateValue
    { 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
            get; 
    }
  }
}
