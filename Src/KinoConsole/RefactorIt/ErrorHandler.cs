// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System;

namespace KinoConsole
{
    public class ErrorHandler
    {
        private Action<int> nativeLib_Error;

        public ErrorHandler(Action<int> nativeLib_Error)
        {
            this.nativeLib_Error = nativeLib_Error;
        }
    }
}