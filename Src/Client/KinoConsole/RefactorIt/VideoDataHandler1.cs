// Type: KinoConsole.RemotePage

using System;
using Windows.Storage.Streams;

public class VideoDataHandler
{
    private Action<IBuffer, uint> nativeLib_VideoData;

    public VideoDataHandler(Action<IBuffer, uint> nativeLib_VideoData)
    {
        this.nativeLib_VideoData = nativeLib_VideoData;
    }
}
