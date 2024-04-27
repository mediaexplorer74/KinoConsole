// Decompiled with JetBrains decompiler
// Type: KinoConsole.VideoMediaStreamSource
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

namespace KinoConsole
{
    public class MediaStreamDescription
    {
        private MediaStreamType mediaStreamType;
        private System.Collections.Generic.IDictionary<MediaStreamAttributeKeys, string> dictionary;

        public MediaStreamDescription(MediaStreamType mediaStreamType, System.Collections.Generic.IDictionary<MediaStreamAttributeKeys, string> dictionary)
        {
            this.mediaStreamType = mediaStreamType;
            this.dictionary = dictionary;
        }
    }
}