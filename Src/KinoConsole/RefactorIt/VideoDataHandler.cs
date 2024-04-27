// Decompiled with JetBrains decompiler
// Type: KinoConsole.VideoMediaStreamSource
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System;
using Windows.Storage.Streams;

namespace KinoConsole
{
    public class VideoDataHandler
    {
        private Action<IBuffer, uint> nativeLib_VideoData;

        public VideoDataHandler(Action<IBuffer, uint> nativeLib_VideoData)
        {
            this.nativeLib_VideoData = nativeLib_VideoData;
        }
    }
}