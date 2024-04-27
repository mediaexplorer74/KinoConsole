// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.IControllerListener
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using GuidAttribute = Windows.Foundation.Metadata.GuidAttribute;

namespace Moga.Windows.Phone
{
    [Guid(288442776, 1198, 16280, 130, 144, 76, 207, 237, 114, 123, 25)]
    [Version(1)]
    public interface IControllerListener
  {
    void OnKeyEvent([In] KeyEvent e);

    void OnMotionEvent([In] MotionEvent e);

    void OnStateEvent([In] StateEvent e);
  }
}
