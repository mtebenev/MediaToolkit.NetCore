namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The result type for get thumbnail task.
  /// </summary>
  public class GetThumbnailResult
  {
    public GetThumbnailResult(byte[] thumbnailData)
    {
      this.ThumbnailData = thumbnailData;
    }

    /// <summary>
    /// The thumbnail data.
    /// </summary>
    public byte[] ThumbnailData { get ; }
  }
}
