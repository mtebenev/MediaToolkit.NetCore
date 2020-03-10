using System.Collections.Generic;

namespace MediaToolkit.Core
{
  /// <summary>
  /// The process factory implementation.
  /// </summary>
  internal class FfProcessFactory : IffProcessFactory
  {
    private readonly string _ffprobeFilePath = @"C:\ffmpeg\ffprobe.exe";
    private readonly string _ffmpegFilePath = @"C:\ffmpeg\ffmpeg.exe";

    public IFfProcess LaunchFfMpeg(IEnumerable<string> arguments)
    {
      IFfProcess ffProcess = new FfProcess(this._ffmpegFilePath, arguments);
      return ffProcess;
    }

    public IFfProcess LaunchFfProbe(IEnumerable<string> arguments)
    {
      IFfProcess ffProcess = new FfProcess(this._ffprobeFilePath, arguments);
      return ffProcess;
    }
  }
}
