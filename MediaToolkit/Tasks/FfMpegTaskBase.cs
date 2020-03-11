using System.Threading.Tasks;
using MediaToolkit.Services;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The base class for all ffmpeg tasks.
  /// </summary>
  public abstract class FfMpegTaskBase<TResult> : FfTaskBase<TResult>
  {
    internal override Task<TResult> ExecuteAsync(MediaToolkitService ffService)
    {
      return ffService.ExecuteAsync(this);
    }
  }
}
