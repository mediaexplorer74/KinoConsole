// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;

namespace KinoConsole
{
    public class CNativeLib
    {
        public EventRegistrationToken add_Connected(ConnectedHandler handler)
        {
            return default;
        }

        public EventRegistrationToken add_Error(ErrorHandler handler)
        {
            return default;
        }

        public EventRegistrationToken add_GameControllerState(GameControllerStateHandler handler)
        {
            return default;
        }

        public EventRegistrationToken add_ListUpdated(ListUpdatedHandler handler)
        {
            return default;
        }

        public EventRegistrationToken add_VideoData(VideoDataHandler handler)
        {
            return default;
        }

        public bool Connect(IBuffer serverUid, IBuffer appPath, bool enableGyroscope)
        {
            return true;
        }

        public void Disconnect()
        {
            //
        }

        public bool GetGameControllerState()
        {
            return false;
        }

        public IBuffer GetListAppIcon(int idx)
        {
            return default;
        }

        public string GetListAppName(int idx)
        {
            return "TestAppName"; ;
        }

        public IBuffer GetListAppPath(int idx)
        {
            return default;
        }

        public string GetListServerName(int idx)
        {
            return "ListServerName";
        }

        public IBuffer GetListServerUid(int idx)
        {
            return default;
        }

        public Status GetListStatus(int idx)
        {
            return default;
        }

        public void JoystickEvent(int joystickEvent, float v)
        {
            //
        }

        public void KeyboardEvent(bool v, int keyCode)
        {
            //
        }

        public void PointerEvent(int v1, bool v2, int x, int y)
        {
            //
        }

        public void remove_Connected(EventRegistrationToken token)
        {
            //
        }

        public void remove_Error(EventRegistrationToken token)
        {
            //
        }

        public void remove_GameControllerState(EventRegistrationToken token)
        {
            //
        }

        public void remove_ListUpdated(EventRegistrationToken token)
        {
            //
        }

        public void remove_VideoData(EventRegistrationToken token)
        {
            //
        }

        public void SetRotation(float x, float y, float z)
        {
            //
        }

        public void StartSearch()
        {
            //
        }

        public void StopSearch()
        {
            //
        }

        internal void Start(object value)
        {
            //
        }

        internal void Stop()
        {
            //
        }
    }
}