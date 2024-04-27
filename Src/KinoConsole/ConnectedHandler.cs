// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System;

namespace KinoConsole
{
    internal class ConnectedHandler
    {
        private Action nativeLib_Connected;

        public ConnectedHandler(Action nativeLib_Connected)
        {
            this.nativeLib_Connected = nativeLib_Connected;
        }
    }
}