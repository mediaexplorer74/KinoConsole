// Managed implementation of Moga KeyEvent using Windows.Gaming.Input
// Converted from P/Invoke to UWP-compatible implementation for Windows 10 Mobile SDK 15063

using System;

namespace Moga.Windows.Phone
{
    public sealed class KeyEvent : EventArgs
    {
        public KeyEvent(KeyCode keyCode, ControllerAction action)
        {
            KeyCode = keyCode;
            Action = action;
        }

        public KeyCode KeyCode { get; }
        public ControllerAction Action { get; }
    }
}
