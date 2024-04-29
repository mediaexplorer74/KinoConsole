// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.__IKeyEventPublicNonVirtuals
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using Windows.Foundation.Metadata;

namespace Moga.Windows.Phone
{
  [ExclusiveTo(typeof (KeyEvent))]
  [Guid(3805013997, 45533, 14704, 133, 158, 125, 206, 95, 48, 148, 217)]
  [Version(1)]
  internal interface IKeyEventPublicNonVirtuals
  {
    KeyCode KeyCode { get; }

    ControllerAction Action { get; }
  }
}
