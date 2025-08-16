// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

namespace KinoConsole
{
    public class ManipulationDeltaEventArgs
    {
        public DeltaManipulation DeltaManipulation;
    }

    public class DeltaManipulation
    {
        public Translation Translation;
    }

    public class Translation
    {
        internal double X;
    }
}