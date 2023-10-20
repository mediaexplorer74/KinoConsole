// Decompiled with JetBrains decompiler
// Type: NativeLib.FlurryEventWithParamHandler
// Assembly: NativeLib, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 26324733-8BE5-4DBE-9852-A1DE1C53477E
// Assembly location: C:\Users\Admin\Desktop\re\KC\NativeLib.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using GuidAttribute = Windows.Foundation.Metadata.GuidAttribute;

namespace NativeLib
{
  [Guid(4250343508, 40817, 14069, 130, 90, 229, 211, 128, 146, 201, 156)]
  [Version(1)]
  public delegate void FlurryEventWithParamHandler(
    [In] string __param0,
    [In] string __param1,
    [In] string __param2);
}
