// Managed implementation of Moga MotionEvent using Windows.Gaming.Input
// Converted from P/Invoke to UWP-compatible implementation for Windows 10 Mobile SDK 15063

using System;

namespace Moga.Windows.Phone
{
    public sealed class MotionEvent : EventArgs
    {
        public MotionEvent(Axis axis, float axisValue)
        {
            Axis = axis;
            AxisValue = axisValue;
        }

        public Axis Axis { get; }

        public float AxisValue { get; }
    }
}
