using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaToolkit.Core;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The tasks extracts the video thumbnail.
  /// </summary>
  public class FfTaskGetThumbnail : FfMpegTaskBase<GetThumbnailResult>
  {
    private readonly string _inputFilePath;
    private readonly TimeSpan _seekSpan;
    private readonly FrameSize _frameSize;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="inputFilePath">Full path to the input video file.</param>
    /// <param name="seekSpan">The frame timespan.</param>
    /// <param name="frameSize">The frame size. Pass null to let ffmpeg guess the frame size.</param>
    public FfTaskGetThumbnail(string inputFilePath, TimeSpan seekSpan, FrameSize frameSize)
    {
      this._inputFilePath = inputFilePath;
      this._seekSpan = seekSpan;
      this._frameSize = frameSize;
    }

    public override IList<string> CreateArguments()
    {
      var arguments = new List<string>
      {
        "-hide_banner",
        "-loglevel",
        "info",
        "-ss",
        this._seekSpan.TotalSeconds.ToString(),
        "-i",
        $@"{this._inputFilePath}",
        "-t",
        "1",
        "-f",
        "gif",
        "-vframes",
        "1"
      };

      if(this._frameSize != null)
      {
        arguments.Add("-s");
        arguments.Add(this._frameSize.ToString());
      }

      arguments.Add("-");

      return arguments;
    }

    public override async Task<GetThumbnailResult> ExecuteCommandAsync(IFfProcess ffProcess)
    {
      await ffProcess.Task;
      byte[] thumbnailData;
      using(var ms = new MemoryStream())
      {
        await ffProcess.OutputReader.BaseStream.CopyToAsync(ms);
        thumbnailData = ms.ToArray();
      }

      return new GetThumbnailResult(thumbnailData);
    }
  }
}
