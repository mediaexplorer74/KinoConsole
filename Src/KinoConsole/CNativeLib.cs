// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;

namespace KinoConsole
{
    internal class CNativeLib
    {
        internal EventRegistrationToken add_Connected(ConnectedHandler handler)
        {
            throw new NotImplementedException();
        }

        internal EventRegistrationToken add_Error(ErrorHandler handler)
        {
            throw new NotImplementedException();
        }

        internal EventRegistrationToken add_GameControllerState(GameControllerStateHandler handler)
        {
            throw new NotImplementedException();
        }

        internal bool Connect(IBuffer serverUid, IBuffer appPath, bool enableGyroscope)
        {
            throw new NotImplementedException();
        }

        internal void Disconnect()
        {
            throw new NotImplementedException();
        }

        internal bool GetGameControllerState()
        {
            throw new NotImplementedException();
        }

        internal void JoystickEvent(int joystickEvent, float v)
        {
            throw new NotImplementedException();
        }

        internal void KeyboardEvent(bool v, int keyCode)
        {
            throw new NotImplementedException();
        }

        internal void PointerEvent(int v1, bool v2, int x, int y)
        {
            throw new NotImplementedException();
        }

        internal void remove_Connected(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        internal void remove_Error(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        internal void remove_GameControllerState(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        internal void SetRotation(float x, float y, float z)
        {
            throw new NotImplementedException();
        }
    }
}