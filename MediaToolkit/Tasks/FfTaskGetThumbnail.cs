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
    private readonly GetThumbnailOptions _options;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="inputFilePath">Full path to the input video file.</param>
    /// <param name="options">The task options.</param>
    public FfTaskGetThumbnail(string inputFilePath, GetThumbnailOptions options)
    {
      this._inputFilePath = inputFilePath;
      this._options = options;
    }

    public override IList<string> CreateArguments()
    {
      var arguments = new List<string>
      {
        "-hide_banner",
        "-loglevel",
        "info",
        "-ss",
        this._options.SeekSpan.TotalSeconds.ToString(),
        "-i",
        $@"{this._inputFilePath}",
        "-t",
        "1"
      };

      arguments.Add("-f");
      arguments.Add(String.IsNullOrEmpty(this._options.OutputFormat)
        ? OutputFormat.RawVideo
        : this._options.OutputFormat);

      if(!String.IsNullOrEmpty(this._options.PixelFormat))
      {
        arguments.Add("-pix_fmt");
        arguments.Add(this._options.PixelFormat);
      }

      arguments.Add("-vframes");
      arguments.Add("1");

      if(this._options.FrameSize != null)
      {
        arguments.Add("-s");
        arguments.Add(this._options.FrameSize.ToString());
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
