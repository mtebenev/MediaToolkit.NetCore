using System.Threading.Tasks;
using MediaToolkit.Services;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The base class for all ffmpeg tasks.
  /// </summary>
  public abstract class FfProbeTaskBase<TResult> : FfTaskBase<TResult>
  {
    internal override Task ExecuteAsync(FfService ffService)
    {
      return ffService.ExecuteAsync(this);
    }
  }
}
