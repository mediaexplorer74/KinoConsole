// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System;

namespace KinoConsole
{
    internal class SensorBase<T>
    {
        internal TimeSpan TimeBetweenUpdates;
        internal EventHandler<SensorReadingEventArgs<MotionReading>> CurrentValueChanged;
        internal bool IsDataValid;

        internal void Start()
        {
            throw new NotImplementedException();
        }

        internal void Stop()
        {
            throw new NotImplementedException();
        }

        public static explicit operator SensorBase<T>(Motion v)
        {
            throw new NotImplementedException();
        }
    }
}