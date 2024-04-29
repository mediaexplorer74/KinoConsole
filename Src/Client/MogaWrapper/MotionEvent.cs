// Type: Moga.Windows.Phone.MotionEvent
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//using Windows.Foundation.Metadata;

namespace Moga.Windows.Phone
{
  //[MarshalingBehavior]
  //[Threading]
  //[Version(1)]
  //[Activatable(typeof (IMotionEventFactory), 1)]
  public sealed class MotionEvent : IMotionEventPublicNonVirtuals
  {
    //[Overload("CreateInstance1")]
    [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "CreateInstance1")]
    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public static extern void CreateInstance1([MarshalAs(UnmanagedType.Interface)] out IControllerManagerPublicNonVirtuals output);

    public extern MotionEvent([In] Axis axis, [In] float axisValue);

    public extern Axis Axis 
    {
        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
        get; 
    }

    public extern float AxisValue 
    { 
        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
        get; 
    }
  }
}
