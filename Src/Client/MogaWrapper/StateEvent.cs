// Managed implementation of Moga StateEvent using Windows.Gaming.Input
// Converted from P/Invoke to UWP-compatible implementation for Windows 10 Mobile SDK 15063

using System;

namespace Moga.Windows.Phone
{
    public sealed class StateEvent : EventArgs
    {
        public StateEvent(ControllerState stateKey, ControllerResult stateValue)
        {
            StateKey = stateKey;
            StateValue = stateValue;
        }

        public ControllerState StateKey { get; }

        public ControllerResult StateValue { get; }
    }
}
