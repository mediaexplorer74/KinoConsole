// Type: NativeLib.CNativeLib
// Assembly: NativeLib, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime

/* 
// Broken code: 
[DllImport(@"C:\path\Name.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "Function")]
public static extern void Function([In, Out] StringBuilder output, [In, MarshalAs(UnmanagedType.SysUInt)] UIntPtr size);

// How to fix? For example,
[DllImport(@"Name", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Function")] 
public static extern void Function([In, Out] StringBuilder output, [In, MarshalAs(UnmanagedType.SysUInt)] UIntPtr size);

 */
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
  public class CNativeLib //: / *IClosable, ICNativeLibPublicNonVirtuals,* / ICNativeLibProtectedNonVirtuals
  {
    //[Overload("CreateInstance1")]
    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "CreateInstance1")]
    static extern void CreateInstance1([MarshalAs(UnmanagedType.Interface)] out ICNativeLibPublicNonVirtuals output);

    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    [DllImport("NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Start")]
    public static extern void Start([In] bool fullVersion);


    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Stop")]
    public static extern void Stop();

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "RemoteSessionStart")]
    public static extern void RemoteSessionStart();

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "RemoteSessionStop")]
    public static extern void RemoteSessionStop();

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "AudioWrite")]
    public static extern void AudioWrite(out ushort pcm, [In] int pcmSize);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "SetRotation")]
    public static extern void SetRotation([In] float x, [In] float y, [In] float z);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Rotation")]
    public static extern float Rotation();

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "StartSearch")]
    public static extern void StartSearch();

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "StopSearch")]
    public static extern void StopSearch();

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "SetScreenSize")]
    public static extern void SetScreenSize([In] int width, [In] int height, [In] int dpi);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Connect")]
    public static extern bool Connect([In] IBuffer serverUid, [In] IBuffer path, [In] bool reportRotation);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Disconnect")]
    public static extern void Disconnect();

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "KeyboardEvent")]
    public static extern void KeyboardEvent([In] bool pressed, [In] int c);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "JoystickEvent")]
    public static extern void JoystickEvent([In] int id, [In] float data);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "PointerEvent")]
    public static extern void PointerEvent([In] int pointerId, [In] bool down, [In] int x, [In] int y);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "SetPassword")]
    public static extern void SetPassword([In] IBuffer serverUid, [In] string password);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "AddServer")]
    public static extern bool AddServer([In] string address);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetGameControllerState")]
    public static extern bool GetGameControllerState();

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "SetMouseMode")]
    public static extern bool SetMouseMode([In] bool mouseMode);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetListServerUid")]
    public static extern IBuffer GetListServerUid([In] int idx);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetListServerName")]
    public static extern string GetListServerName([In] int idx);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetListStatus")]
    public static extern int GetListStatus([In] int idx);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetListAppName")]
    public static extern string GetListAppName([In] int idx);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetListAppPath")]
    public static extern IBuffer GetListAppPath([In] int idx);

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetListAppIcon")]
    public static extern IBuffer GetListAppIcon([In] int idx);


    public extern event ListUpdatedHandler ListUpdated;

    public extern event VideoDataHandler VideoData;

    public extern event ConnectedHandler Connected;

    public extern event ErrorHandler Error;

    public extern event GameControllerStateHandler GameControllerState;

    public extern event FlurryEventHandler FlurryEvent;

    public extern event FlurryEventWithParamHandler FlurryEventWithParam;

    public extern event FlurryErrorHandler FlurryError;

    [DllImport(@"NativeLib", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "Close")]
    public static extern void Close();
  }
}
