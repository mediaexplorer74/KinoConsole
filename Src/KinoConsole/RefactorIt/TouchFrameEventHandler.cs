// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

namespace KinoConsole
{
    internal class TouchFrameEventHandler
    {
        private System.Action<object, TouchFrameEventArgs> touch_FrameReported;

        public TouchFrameEventHandler(System.Action<object, TouchFrameEventArgs> touch_FrameReported)
        {
            this.touch_FrameReported = touch_FrameReported;
        }
    }
}