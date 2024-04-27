// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using Windows.Foundation;

namespace KinoConsole
{
    public class TouchPoint
    {
        public int Action;
        public Point Position;
        public TouchDevice TouchDevice;
    }

    public class TouchDevice
    {
        internal int Id;
    }
}