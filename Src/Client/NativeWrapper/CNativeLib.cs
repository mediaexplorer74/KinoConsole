// Managed adapter wrapper for NativeLib WinRT component
// Provides compatibility with existing P/Invoke-based CNativeLib API

using System;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace NativeLib
{
  /// <summary>
  /// Managed adapter that wraps NativeLib WinRT component
  /// Maintains compatibility with existing P/Invoke-based API
  /// </summary>
  public class CNativeLib
  {
    private static ICNativeLibPublicNonVirtuals _nativeInstance;
    private static readonly object _lockObject = new object();

    static CNativeLib()
    {
      InitializeNativeInstance();
    }

    private static void InitializeNativeInstance()
    {
      lock (_lockObject)
      {
        if (_nativeInstance == null)
        {
          // Activate WinRT class NativeLib.CNativeLib from NativeLib.winmd (WindowsRuntime component)
          Type rtType = null;

          try
          {
                rtType = Type.GetType("NativeLib.CNativeLib, NativeLib, ContentType=WindowsRuntime");
          }
          catch (Exception ex)
          {
                Debug.WriteLine($"Error loading type 'NativeLib.CNativeLib': {ex.Message}");
          }

          if (rtType == null)
          {
            Debug.WriteLine("Cannot resolve WinRT type 'NativeLib.CNativeLib' from 'NativeLib.winmd'.");
          }

            IActivationFactory factoryObj = default;

            try
            {
                factoryObj = WindowsRuntimeMarshal.GetActivationFactory(rtType);
            }
            catch { }

            IActivationFactory factory = default;

            try
            {
                factory = factoryObj as IActivationFactory;
            }
            catch { }

          if (factory == null)
          {
            Debug.WriteLine("Activation factory for 'NativeLib.CNativeLib' is not available.");

            return;
          }

          object instance = factory.ActivateInstance();

          _nativeInstance = instance as ICNativeLibPublicNonVirtuals;
          if (_nativeInstance == null)
          {
            throw new InvalidCastException("Activated instance does not implement ICNativeLibPublicNonVirtuals.");
          }
        }
      }
    }

    // Methods
    public static void Start(bool fullVersion)
    {
      _nativeInstance?.Start(fullVersion);
    }

    public static void Stop()
    {
      _nativeInstance?.Stop();
    }

    public static void RemoteSessionStart()
    {
      _nativeInstance?.RemoteSessionStart();
    }

    public static void RemoteSessionStop()
    {
      _nativeInstance?.RemoteSessionStop();
    }

    public static void AudioWrite(out ushort pcm, int pcmSize)
    {
      ushort temp = 0;
      var inst = _nativeInstance;
      if (inst != null)
      {
        inst.AudioWrite(out temp, pcmSize);
      }
      pcm = temp;
    }

    public static void SetRotation(float x, float y, float z)
    {
      _nativeInstance?.SetRotation(x, y, z);
    }

    public static float Rotation()
    {
      return _nativeInstance?.Rotation() ?? 0.0f;
    }

    public static void StartSearch()
    {    

        try
        {
            _nativeInstance?.StartSearch();
        }
        catch //(Exception ex)
        {
            // Log or handle initialization error
            Debug.WriteLine($"[ex] Critical error in _nativeInstance?.StartSearch() ");// : {ex.Message}");           
        }
    }

    public static void StopSearch()
    {
      _nativeInstance?.StopSearch();
    }

    public static void SetScreenSize(int width, int height, int dpi)
    {
      _nativeInstance?.SetScreenSize(width, height, dpi);
    }

    public static bool Connect(IBuffer serverUid, IBuffer path, bool reportRotation)
    {
      return _nativeInstance?.Connect(serverUid, path, reportRotation) ?? false;
    }

    public static void Disconnect()
    {
      _nativeInstance?.Disconnect();
    }

    public static void KeyboardEvent(bool pressed, int c)
    {
      _nativeInstance?.KeyboardEvent(pressed, c);
    }

    public static void JoystickEvent(int id, float data)
    {
      _nativeInstance?.JoystickEvent(id, data);
    }

    public static void PointerEvent(int pointerId, bool down, int x, int y)
    {
      _nativeInstance?.PointerEvent(pointerId, down, x, y);
    }

    public static void SetPassword(IBuffer serverUid, string password)
    {
      _nativeInstance?.SetPassword(serverUid, password);
    }

    public static bool AddServer(string address)
    {
      return _nativeInstance?.AddServer(address) ?? false;
    }

    public static bool GetGameControllerState()
    {
      return _nativeInstance?.GetGameControllerState() ?? false;
    }

    public static bool SetMouseMode(bool mouseMode)
    {
      return _nativeInstance?.SetMouseMode(mouseMode) ?? false;
    }

    public static IBuffer GetListServerUid(int idx)
    {
      return _nativeInstance?.GetListServerUid(idx);
    }

    public static string GetListServerName(int idx)
    {
      return _nativeInstance?.GetListServerName(idx);
    }

    public static int GetListStatus(int idx)
    {
      return _nativeInstance?.GetListStatus(idx) ?? 0;
    }

    public static string GetListAppName(int idx)
    {
      return _nativeInstance?.GetListAppName(idx);
    }

    public static IBuffer GetListAppPath(int idx)
    {
      return _nativeInstance?.GetListAppPath(idx);
    }

    public static IBuffer GetListAppIcon(int idx)
    {
      return _nativeInstance?.GetListAppIcon(idx);
    }

    public static void Close()
    {
      // Интерфейс WinRT не содержит Close, используем Stop как безопасный аналог
      Stop();
    }

    // Events - forwarded from WinRT instance
    public static event ListUpdatedHandler ListUpdated
    {
      add { if (_nativeInstance != null) _nativeInstance.ListUpdated += value; }
      remove { if (_nativeInstance != null) _nativeInstance.ListUpdated -= value; }
    }

    public static event VideoDataHandler VideoData
    {
      add { if (_nativeInstance != null) _nativeInstance.VideoData += value; }
      remove { if (_nativeInstance != null) _nativeInstance.VideoData -= value; }
    }

    public static event ConnectedHandler Connected
    {
      add { if (_nativeInstance != null) _nativeInstance.Connected += value; }
      remove { if (_nativeInstance != null) _nativeInstance.Connected -= value; }
    }

    public static event ErrorHandler Error
    {
      add { if (_nativeInstance != null) _nativeInstance.Error += value; }
      remove { if (_nativeInstance != null) _nativeInstance.Error -= value; }
    }

    public static event GameControllerStateHandler GameControllerState
    {
      add { if (_nativeInstance != null) _nativeInstance.GameControllerState += value; }
      remove { if (_nativeInstance != null) _nativeInstance.GameControllerState -= value; }
    }

    public static event FlurryEventHandler FlurryEvent
    {
      add { if (_nativeInstance != null) _nativeInstance.FlurryEvent += value; }
      remove { if (_nativeInstance != null) _nativeInstance.FlurryEvent -= value; }
    }

    public static event FlurryEventWithParamHandler FlurryEventWithParam
    {
      add { if (_nativeInstance != null) _nativeInstance.FlurryEventWithParam += value; }
      remove { if (_nativeInstance != null) _nativeInstance.FlurryEventWithParam -= value; }
    }

    public static event FlurryErrorHandler FlurryError
    {
      add { if (_nativeInstance != null) _nativeInstance.FlurryError += value; }
      remove { if (_nativeInstance != null) _nativeInstance.FlurryError -= value; }
    }
  }
}