// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.__IMotionEventFactory
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using GuidAttribute = Windows.Foundation.Metadata.GuidAttribute;

namespace Moga.Windows.Phone
{
  [ExclusiveTo(typeof (MotionEvent))]
  [Version(1)]
  [Guid(4175825627, 57401, 15901, 152, 79, 106, 120, 160, 98, 65, 7)]
  internal interface IMotionEventFactory
  {
    [Overload("CreateInstance1")]
    MotionEvent CreateInstance([In] Axis axis, [In] float axisValue);
  }
}
