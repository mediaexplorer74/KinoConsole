// Decompiled with JetBrains decompiler
// Type: KinoConsole.VideoMediaStreamSource
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System.Collections.Generic;
using System.IO;

namespace KinoConsole
{
    internal class MediaStreamSample
    {
        private MediaStreamDescription videoDesc;
        private Stream stream;
        private long v1;
        private long v2;
        private long v3;
        private long v4;
        private IDictionary<MediaSampleAttributeKeys, string> emptySampleDict;
        private long v;
        private long length;
        private long hnsPresentationTime;

        public MediaStreamSample(MediaStreamDescription videoDesc, Stream stream, long v, long length, long hnsPresentationTime, IDictionary<MediaSampleAttributeKeys, string> emptySampleDict)
        {
            this.videoDesc = videoDesc;
            this.stream = stream;
            this.v = v;
            this.length = length;
            this.hnsPresentationTime = hnsPresentationTime;
            this.emptySampleDict = emptySampleDict;
        }

        public MediaStreamSample(MediaStreamDescription videoDesc, Stream stream, long v1, long v2, long v3, long v4, IDictionary<MediaSampleAttributeKeys, string> emptySampleDict)
        {
            this.videoDesc = videoDesc;
            this.stream = stream;
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            this.emptySampleDict = emptySampleDict;
        }
    }
}