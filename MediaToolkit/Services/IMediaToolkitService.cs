using System.Threading.Tasks;
using MediaToolkit.Tasks;

namespace MediaToolkit.Services
{
  /// <summary>
  /// The service for invoking commands for ffmpeg and ffprobe.
  /// </summary>
  public interface IMediaToolkitService
  {
    Task<TResult> ExecuteAsync<TResult>(FfTaskBase<TResult> task);
  }
}
