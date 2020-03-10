using MediaToolkit.Model;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The result type for get metadata task.
  /// </summary>
  public class GetMetadataResult
  {
    public GetMetadataResult(FfprobeType metadata)
    {
      this.Metadata = metadata;
    }

    /// <summary>
    /// The result metadata.
    /// </summary>
    public FfprobeType Metadata { get; private set; }
  }
}
