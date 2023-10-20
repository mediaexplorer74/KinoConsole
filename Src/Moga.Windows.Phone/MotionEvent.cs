// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.MotionEvent
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
  [Version(1)]
  [Activatable(typeof (IMotionEventFactory), 1)]
  public sealed class MotionEvent : IMotionEventPublicNonVirtuals
  {
    //[Overload("CreateInstance1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern MotionEvent([In] Axis axis, [In] float axisValue);

    public extern Axis Axis { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern float AxisValue { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }
  }
}
