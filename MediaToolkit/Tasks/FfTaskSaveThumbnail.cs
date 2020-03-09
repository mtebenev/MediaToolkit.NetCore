using System;
using System.Threading.Tasks;
using MediaToolkit.Core;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The task saves the video thumbnail.
  /// </summary>
  public class FfTaskSaveThumbnail : FfMpegTaskBase
  {
    private readonly string _filePath;
    private readonly string _outputFilePath;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="inputFilePath">Full path to the input video file.</param>
    /// <param name="outputFilePath">Full path to the output video file.</param>
    public FfTaskSaveThumbnail(string inputFilePath, string outputFilePath)
    {
      this._filePath = inputFilePath;
      this._outputFilePath = outputFilePath;
    }

    public override string[] CreateArguments()
    {
      TimeSpan seekSpan = TimeSpan.FromSeconds(1);

      var arguments = new[]
      {
        "-nostdin",
        "-y",
        "-loglevel",
        "info",
        "-ss",
        seekSpan.TotalSeconds.ToString(),
        "-i",
        this._filePath,
        "-vframes",
        "1",
      };

      return arguments;
    }

    public override async Task ExecuteCommandAsync(IFfProcess ffProcess)
    {
      await ffProcess.Run();
    }
  }
}
