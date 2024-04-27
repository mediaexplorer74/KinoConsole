// Type: NativeLib.CNativeLib
// Assembly: NativeLib, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Storage.Streams;

namespace NativeLib
{
 //[Version(1)]
 //[Threading]
 //[MarshalingBehavior]
 //[Activatable(1)]
  public class CNativeLib //: 
    //IClosable,
    //ICNativeLibPublicNonVirtuals,
    //ICNativeLibProtectedNonVirtuals
  {
        //[Overload("CreateInstance1")]
        //[MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        static extern CNativeLib();

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        //[MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public /*extern*/ void Start([In] bool fullVersion)
        { }

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void Stop();

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void RemoteSessionStart();

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void RemoteSessionStop();

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void AudioWrite(out ushort pcm, [In] int pcmSize);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void SetRotation([In] float x, [In] float y, [In] float z);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern float Rotation();

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void StartSearch();

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void StopSearch();

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void SetScreenSize([In] int width, [In] int height, [In] int dpi);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern bool Connect([In] IBuffer serverUid, [In] IBuffer path, [In] bool reportRotation);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void Disconnect();

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void KeyboardEvent([In] bool pressed, [In] int c);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void JoystickEvent([In] int id, [In] float data);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void PointerEvent([In] int pointerId, [In] bool down, [In] int x, [In] int y);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern void SetPassword([In] IBuffer serverUid, [In] string password);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern bool AddServer([In] string address);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern bool GetGameControllerState();

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern bool SetMouseMode([In] bool mouseMode);

        // [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern IBuffer GetListServerUid([In] int idx);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern string GetListServerName([In] int idx);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern int GetListStatus([In] int idx);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern string GetListAppName([In] int idx);


        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern IBuffer GetListAppPath([In] int idx);

        //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public extern IBuffer GetListAppIcon([In] int idx);

    public extern event ListUpdatedHandler ListUpdated;

    public extern event VideoDataHandler VideoData;

    public extern event ConnectedHandler Connected;

    public extern event ErrorHandler Error;

    public extern event GameControllerStateHandler GameControllerState;

    public extern event FlurryEventHandler FlurryEvent;

    public extern event FlurryEventWithParamHandler FlurryEventWithParam;

    public extern event FlurryErrorHandler FlurryError;

    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
    public extern void Close();
  }
}
