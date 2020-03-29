namespace MediaToolkit.Services
{
  /// <summary>
  /// The MediaToolkit configuration.
  /// </summary>
  public class MediaToolkitOptions
  {
    /// <summary>
    /// The absolute path to the ffmpeg.exe
    /// </summary>
    public string FfMpegPath { get; set; }

    /// <summary>
    /// The absolute path to the ffprobe.
    /// Pass null to use ffprobe.exe in the same directory as the ffmpeg (works on Windows only)
    /// </summary>
    /// <value></value>
    public string FfProbePath { get; set; }
  }
}
