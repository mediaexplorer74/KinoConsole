// Type: NativeLib.__ICNativeLibPublicNonVirtuals

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using Windows.Storage.Streams;
using GuidAttribute = Windows.Foundation.Metadata.GuidAttribute;

namespace NativeLib
{
  [Version(1)]
  [ExclusiveTo(typeof (CNativeLib))]
  [Guid(4156959502, 62164, 15672, 160, 218, 90, 181, 104, 77, 111, 97)]
  public interface ICNativeLibPublicNonVirtuals
  {
    void Start([In] bool fullVersion);

    void Stop();

    void RemoteSessionStart();

    void RemoteSessionStop();

    void AudioWrite(out ushort pcm, [In] int pcmSize);

    void SetRotation([In] float x, [In] float y, [In] float z);

    float Rotation();

    void StartSearch();

    void StopSearch();

    void SetScreenSize([In] int width, [In] int height, [In] int dpi);

    bool Connect([In] IBuffer serverUid, [In] IBuffer path, [In] bool reportRotation);

    void Disconnect();

    void KeyboardEvent([In] bool pressed, [In] int c);

    void JoystickEvent([In] int id, [In] float data);

    void PointerEvent([In] int pointerId, [In] bool down, [In] int x, [In] int y);

    void SetPassword([In] IBuffer serverUid, [In] string password);

    bool AddServer([In] string address);

    bool GetGameControllerState();

    bool SetMouseMode([In] bool mouseMode);

    IBuffer GetListServerUid([In] int idx);

    string GetListServerName([In] int idx);

    int GetListStatus([In] int idx);

    string GetListAppName([In] int idx);

    IBuffer GetListAppPath([In] int idx);

    IBuffer GetListAppIcon([In] int idx);

    event ListUpdatedHandler ListUpdated;

    event VideoDataHandler VideoData;

    event ConnectedHandler Connected;

    event ErrorHandler Error;

    event GameControllerStateHandler GameControllerState;

    event FlurryEventHandler FlurryEvent;

    event FlurryEventWithParamHandler FlurryEventWithParam;

    event FlurryErrorHandler FlurryError;
  }
}
