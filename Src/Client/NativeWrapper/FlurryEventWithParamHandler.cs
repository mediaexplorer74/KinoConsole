// Type: NativeLib.FlurryEventWithParamHandler
// Assembly: NativeLib, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime

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
