// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System;

namespace KinoConsole
{
    internal class GameControllerStateHandler
    {
        private Action<bool> nativeLib_GameControllerState;

        public GameControllerStateHandler(Action<bool> nativeLib_GameControllerState)
        {
            this.nativeLib_GameControllerState = nativeLib_GameControllerState;
        }
    }
}