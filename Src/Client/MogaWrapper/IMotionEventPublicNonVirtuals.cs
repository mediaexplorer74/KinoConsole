// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.__IMotionEventPublicNonVirtuals
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using Windows.Foundation.Metadata;

namespace Moga.Windows.Phone
{
  [ExclusiveTo(typeof (MotionEvent))]
  [Version(1)]
  [Guid(3802268498, 33392, 15355, 138, 141, 181, 89, 73, 17, 112, 67)]
  internal interface IMotionEventPublicNonVirtuals
  {
    Axis Axis { get; }

    float AxisValue { get; }
  }
}
