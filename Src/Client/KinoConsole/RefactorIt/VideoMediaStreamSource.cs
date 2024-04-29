// Type: KinoConsole.VideoMediaStreamSource
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

//using NativeLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Windows;
using Windows.Storage.Streams;
using Windows.UI.Xaml;

namespace KinoConsole
{
  public class VideoMediaStreamSource : MediaStreamSource, IDisposable
  {
    private const int maxQueueSize = 30;
    private int _frameWidth;
    private int _frameHeight;
    private bool isDisposed;
    private Queue<VideoMediaStreamSource.VideoSample> _sampleQueue;
    private object lockObj = new object();
    private ManualResetEvent shutdownEvent;
    private int _outstandingGetVideoSampleCount;
    private MediaStreamDescription _videoDesc;
    private Dictionary<MediaSampleAttributeKeys, string> _emptySampleDict 
            = new Dictionary<MediaSampleAttributeKeys, string>();

    public VideoMediaStreamSource(int frameWidth, int frameHeight)
    {
      this._frameWidth = frameWidth;
      this._frameHeight = frameHeight;
      this.shutdownEvent = new ManualResetEvent(false);
      this._sampleQueue = new Queue<VideoMediaStreamSource.VideoSample>(30);
      this._outstandingGetVideoSampleCount = 0;
      CNativeLib nativeLib = default;//(Application.Current as App).nativeLib;

            //nativeLib.VideoData += new VideoDataHandler(null, null, null); 
      //WindowsRuntimeMarshal.AddEventHandler<VideoDataHandler>(
      //    new Func<VideoDataHandler, EventRegistrationToken>(nativeLib.add_VideoData), 
      //    new Action<EventRegistrationToken>(nativeLib.remove_VideoData), 
      //    new VideoDataHandler(this.nativeLib_VideoData));
    }

    public void Dispose()
    {
      if (!this.isDisposed)
      {
        //WindowsRuntimeMarshal.RemoveEventHandler<VideoDataHandler>(
        //    new Action<EventRegistrationToken>((Application.Current as App).nativeLib.remove_VideoData), 
        //    new VideoDataHandler(this.nativeLib_VideoData));
        this.isDisposed = true;
      }
      GC.SuppressFinalize((object) this);
    }

    public void Shutdown()
    {
      this.shutdownEvent.Set();
      lock (this.lockObj)
      {
        if (this._outstandingGetVideoSampleCount <= 0)
          return;
        this.ReportGetSampleCompleted(new MediaStreamSample(this._videoDesc, (Stream) null, 0L, 0L, 0L, 0L, (IDictionary<MediaSampleAttributeKeys, string>) this._emptySampleDict));
        this._outstandingGetVideoSampleCount = 0;
      }
    }

        private void ReportGetSampleCompleted(MediaStreamSample mediaStreamSample)
        {
            //throw new NotImplementedException();
        }

        private void nativeLib_VideoData(IBuffer ibuffer, uint userData)
    {
      lock (this.lockObj)
      {
        if (this._sampleQueue.Count >= 30)
          this._sampleQueue.Dequeue();
        this._sampleQueue.Enqueue(new VideoMediaStreamSource.VideoSample(ibuffer, (long) userData));
        this.SendSamples();
      }
    }

    private void SendSamples()
    {
      for (; this._sampleQueue.Count<VideoMediaStreamSource.VideoSample>() > 0 && this._outstandingGetVideoSampleCount > 0 && !this.shutdownEvent.WaitOne(0); --this._outstandingGetVideoSampleCount)
      {
        VideoMediaStreamSource.VideoSample videoSample = this._sampleQueue.Dequeue();
        Stream stream = videoSample.buffer.AsStream();
        this.ReportGetSampleCompleted(new MediaStreamSample(this._videoDesc, stream, 0L, stream.Length, videoSample.hnsPresentationTime, (IDictionary<MediaSampleAttributeKeys, string>) this._emptySampleDict));
      }
    }

    private void PrepareVideo() => this._videoDesc = 
            new MediaStreamDescription((MediaStreamType) 1,
        (IDictionary<MediaStreamAttributeKeys, string>) new Dictionary<MediaStreamAttributeKeys, string>()
    {
      [(MediaStreamAttributeKeys) 1] = "H264",
      [(MediaStreamAttributeKeys) 3] = this._frameHeight.ToString(),
      [(MediaStreamAttributeKeys) 2] = this._frameWidth.ToString()
    });

    private void PrepareAudio()
    {
    }

    protected virtual void OpenMediaAsync()
    {
      Dictionary<MediaSourceAttributesKeys, string> dictionary = 
                new Dictionary<MediaSourceAttributesKeys, string>();
      List<MediaStreamDescription> streamDescriptionList = new List<MediaStreamDescription>();
      this.PrepareVideo();
      streamDescriptionList.Add(this._videoDesc);
      dictionary[(MediaSourceAttributesKeys) 1] = TimeSpan.FromSeconds(0.0).Ticks.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      dictionary[(MediaSourceAttributesKeys) 0] = false.ToString();
      this.ReportOpenMediaCompleted((IDictionary<MediaSourceAttributesKeys, string>) dictionary, (IEnumerable<MediaStreamDescription>) streamDescriptionList);
    }

    private void ReportOpenMediaCompleted(IDictionary<MediaSourceAttributesKeys, string> dictionary, IEnumerable<MediaStreamDescription> streamDescriptionList)
    {
        throw new NotImplementedException();
    }

    public /*virtual*/ void GetSampleAsync(MediaStreamType mediaStreamType)
    {
      //if (mediaStreamType != (MediaStreamType)1)
      //  return;
      lock (this.lockObj)
      {
        ++this._outstandingGetVideoSampleCount;
        this.SendSamples();
      }
    }

    protected virtual void CloseMedia()
    {
    }

        protected virtual void GetDiagnosticAsync(MediaStreamSourceDiagnosticKind diagnosticKind)
        {
            throw new NotImplementedException();
        }

        protected virtual void SwitchMediaStreamAsync(MediaStreamDescription mediaStreamDescription)
        {
            throw new NotImplementedException();
        }

        protected virtual void SeekAsync(long seekToTime)
        {
            this.ReportSeekCompleted(seekToTime);
        }

        private void ReportSeekCompleted(long seekToTime)
        {
            throw new NotImplementedException();
        }

        public class VideoSample
    {
      public IBuffer buffer;
      public long hnsPresentationTime;

      public VideoSample(IBuffer _buffer, long userData)
      {
        this.buffer = _buffer;
        this.hnsPresentationTime = 0L;
      }
    }
  }
}
