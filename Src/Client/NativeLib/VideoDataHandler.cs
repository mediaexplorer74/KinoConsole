// Type: NativeLib.VideoDataHandler
// Assembly: NativeLib, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using Windows.Storage.Streams;
using GuidAttribute = Windows.Foundation.Metadata.GuidAttribute;

namespace NativeLib
{
  [Guid(2685061279, 61602, 14141, 169, 250, 2, 61, 142, 177, 130, 132)]
  [Version(1)]
  public delegate void VideoDataHandler([In] IBuffer __param0, [In] uint __param1);
}
