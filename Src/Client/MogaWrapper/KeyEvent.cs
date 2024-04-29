// Decompiled with JetBrains decompiler
// Type: Moga.Windows.Phone.KeyEvent
// Assembly: Moga.Windows.Phone, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 5159B5F5-77BC-411F-BBE9-8A169DED3F77
// Assembly location: C:\Users\Admin\Desktop\re\KC\Moga.Windows.Phone.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace Moga.Windows.Phone
{
  [Version(1)]
  [Activatable(typeof (IKeyEventFactory), 1)]
  //[Threading]
  //[MarshalingBehavior]
  public sealed class KeyEvent : IKeyEventPublicNonVirtuals
  {
        //[Overload("CreateInstance1")]
        [DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "CreateInstance1")]
        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        public static extern void CreateInstance1([MarshalAs(UnmanagedType.Interface)] out IControllerManagerPublicNonVirtuals output);

        //[DllImport(@"Moga.Windows.Phone", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "KeyEvent")]
        public extern KeyEvent([In] KeyCode keyCode, [In] ControllerAction action);

    public extern KeyCode KeyCode 
    {
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            get; 
    }

    public extern ControllerAction Action
    { 
           //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
           get; 
    }
  }
}
