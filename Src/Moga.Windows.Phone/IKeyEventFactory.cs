// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.__IKeyEventFactory
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using GuidAttribute = Windows.Foundation.Metadata.GuidAttribute;

namespace Moga.Windows.Phone
{
  [ExclusiveTo(typeof (KeyEvent))]
  [Guid(4242278009, 15038, 15527, 151, 50, 5, 160, 14, 241, 129, 94)]
  [Version(1)]
  internal interface IKeyEventFactory
  {
    [Overload("CreateInstance1")]
    KeyEvent CreateInstance([In] KeyCode keyCode, [In] ControllerAction action);
  }
}
