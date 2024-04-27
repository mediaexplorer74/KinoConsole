// Decompiled with JetBrains decompiler
// Type: KinoConsole.MainPage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System;

namespace KinoConsole
{
    public class ListUpdatedHandler
    {
        private Action nativeLib_ListUpdated;

        public ListUpdatedHandler(Action nativeLib_ListUpdated)
        {
            this.nativeLib_ListUpdated = nativeLib_ListUpdated;
        }
    }
}