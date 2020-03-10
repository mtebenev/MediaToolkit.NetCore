using System.Threading.Tasks;
using MediaToolkit.Core;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The tasks extracts the video thumbnail
  /// </summary>
  public class FfTaskGetThumbnail : FfMpegTaskBase<GetThumbnailResult>
  {
    public override string[] CreateArguments()
    {
      throw new System.NotImplementedException();
    }

    public override Task<GetThumbnailResult> ExecuteCommandAsync(IFfProcess ffProcess)
    {
      throw new System.NotImplementedException();
    }
  }
}
