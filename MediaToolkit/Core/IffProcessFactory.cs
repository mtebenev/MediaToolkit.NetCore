using System.Collections.Generic;

namespace MediaToolkit.Core
{
  /// <summary>
  /// Factory service for FF process
  /// </summary>
  internal interface IffProcessFactory
  {
    /// <summary>
    /// Launches the ffmpeg
    /// </summary>
    IFfProcess LaunchFfMpeg(IEnumerable<string> arguments);

    /// <summary>
    /// Launches the ffprobe
    /// </summary>
    IFfProcess LaunchFfProbe(IEnumerable<string> arguments);
  }
}
