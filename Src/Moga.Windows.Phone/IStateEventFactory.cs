// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.__IStateEventFactory
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using GuidAttribute = Windows.Foundation.Metadata.GuidAttribute;

namespace Moga.Windows.Phone
{
  [Guid(2306738990, 26685, 12675, 167, 87, 222, 121, 42, 56, 17, 92)]
  [Version(1)]
  [ExclusiveTo(typeof (StateEvent))]
  internal interface IStateEventFactory
  {
    [Overload("CreateInstance1")]
    StateEvent CreateInstance([In] ControllerState stateKey, [In] ControllerResult stateValue);
  }
}
