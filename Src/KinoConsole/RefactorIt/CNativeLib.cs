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
            throw new NotImplementedException();
        }

        public EventRegistrationToken add_Error(ErrorHandler handler)
        {
            throw new NotImplementedException();
        }

        public EventRegistrationToken add_GameControllerState(GameControllerStateHandler handler)
        {
            throw new NotImplementedException();
        }

        public EventRegistrationToken add_ListUpdated(ListUpdatedHandler handler)
        {
            throw new NotImplementedException();
        }

        public EventRegistrationToken add_VideoData(VideoDataHandler handler)
        {
            throw new NotImplementedException();
        }

        public bool Connect(IBuffer serverUid, IBuffer appPath, bool enableGyroscope)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool GetGameControllerState()
        {
            throw new NotImplementedException();
        }

        public IBuffer GetListAppIcon(int idx)
        {
            throw new NotImplementedException();
        }

        public string GetListAppName(int idx)
        {
            throw new NotImplementedException();
        }

        public IBuffer GetListAppPath(int idx)
        {
            throw new NotImplementedException();
        }

        public string GetListServerName(int idx)
        {
            throw new NotImplementedException();
        }

        public IBuffer GetListServerUid(int idx)
        {
            throw new NotImplementedException();
        }

        public Status GetListStatus(int idx)
        {
            throw new NotImplementedException();
        }

        public void JoystickEvent(int joystickEvent, float v)
        {
            throw new NotImplementedException();
        }

        public void KeyboardEvent(bool v, int keyCode)
        {
            throw new NotImplementedException();
        }

        public void PointerEvent(int v1, bool v2, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void remove_Connected(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_Error(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_GameControllerState(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_ListUpdated(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_VideoData(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void SetRotation(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void StartSearch()
        {
            throw new NotImplementedException();
        }

        public void StopSearch()
        {
            throw new NotImplementedException();
        }

        internal void Start(object value)
        {
            throw new NotImplementedException();
        }

        internal void Stop()
        {
            throw new NotImplementedException();
        }
    }
}