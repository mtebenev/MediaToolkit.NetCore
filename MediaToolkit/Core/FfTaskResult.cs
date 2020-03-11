namespace MediaToolkit.Core
{
  /// <summary>
  /// The ffmpeg/ffprobe execution result
  /// </summary>
  public class FfTaskResult
  {
    public FfTaskResult(string output, string error)
    {
      this.Output = output;
      this.Error = error;
    }

    /// <summary>
    /// The standard output.
    /// </summary>
    public string Output { get; private set; }

    /// <summary>
    /// The error output.
    /// </summary>
    public string Error { get; private set; }
  }
}
