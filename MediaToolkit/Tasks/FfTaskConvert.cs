using System.Threading.Tasks;
using MediaToolkit.Core;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The task converts a video file.
  /// </summary>
  public class FfTaskConvert : FfMpegTaskBase<int>
  {
    public override string[] CreateArguments()
    {
      throw new System.NotImplementedException();
    }

    public override Task<int> ExecuteCommandAsync(IFfProcess ffProcess)
    {
      throw new System.NotImplementedException();
    }
  }
}
