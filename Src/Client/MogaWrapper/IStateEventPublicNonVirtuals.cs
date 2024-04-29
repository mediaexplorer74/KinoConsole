// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.__IStateEventPublicNonVirtuals
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using Windows.Foundation.Metadata;

namespace Moga.Windows.Phone
{
  [Guid(2585446009, 5591, 14719, 139, 220, 192, 84, 194, 151, 185, 237)]
  [Version(1)]
  [ExclusiveTo(typeof (StateEvent))]
  internal interface IStateEventPublicNonVirtuals
  {
    ControllerState StateKey { get; }

    ControllerResult StateValue { get; }
  }
}
