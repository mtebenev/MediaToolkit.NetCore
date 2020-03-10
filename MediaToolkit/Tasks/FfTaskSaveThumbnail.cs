using System;
using System.Threading.Tasks;
using MediaToolkit.Core;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The task saves the video thumbnail.
  /// The result is a dummy value.
  /// </summary>
  public class FfTaskSaveThumbnail : FfMpegTaskBase<int>
  {
    private readonly string _inputFilePath;
    private readonly string _outputFilePath;
    private readonly TimeSpan _seekSpan;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="inputFilePath">Full path to the input video file.</param>
    /// <param name="outputFilePath">Full path to the output video file.</param>
    /// <param name="seekSpan">The frame timespan.</param>
    public FfTaskSaveThumbnail(string inputFilePath, string outputFilePath, TimeSpan seekSpan)
    {
      this._inputFilePath = inputFilePath;
      this._outputFilePath = outputFilePath;
      this._seekSpan = seekSpan;
    }

    public override string[] CreateArguments()
    {
      var arguments = new[]
      {
        "-nostdin",
        "-y",
        "-loglevel",
        "info",
        "-ss",
        this._seekSpan.TotalSeconds.ToString(),
        "-i",
        $@"{this._inputFilePath}",
        "-vframes",
        "1",
        $@"{this._outputFilePath}",
      };

      return arguments;
    }

    /// <summary>
    /// FfTaskBase.
    /// </summary>
    public override async Task<int> ExecuteCommandAsync(IFfProcess ffProcess)
    {
      await ffProcess.Run();
      return 0;
    }
  }
}
