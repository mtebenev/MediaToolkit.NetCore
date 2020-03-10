using System.Threading.Tasks;
using MediaToolkit.Core;
using MediaToolkit.Tasks;

namespace MediaToolkit.Services
{
  /// <summary>
  /// The FF service implementation.
  /// </summary>
  public class FfService : IFfService
  {
    private readonly IffProcessFactory _processFactory;

    /// <summary>
    /// Ctor.
    /// </summary>
    internal FfService(IffProcessFactory processFactory)
    {
      this._processFactory = processFactory;
    }

    /// <summary>
    /// Factory method.
    /// </summary>
    public static IFfService CreateInstance()
    {
      var ffProcessFactory = new FfProcessFactory();
      var result = new FfService(ffProcessFactory);
      return result;
    }

    public Task<TResult> ExecuteAsync<TResult>(FfTaskBase<TResult> task)
    {
      var result = task.ExecuteAsync(this);
      return result;
    }

    /// <summary>
    /// Dispatcher for ffprobe tasks.
    /// </summary>
    internal Task<TResult> ExecuteAsync<TResult>(FfProbeTaskBase<TResult> task)
    {
      var arguments = task.CreateArguments();
      var ffProcess = this._processFactory.LaunchFfProbe(arguments);

      return task.ExecuteCommandAsync(ffProcess);
    }

    /// <summary>
    /// Dispatcher for ffmpeg tasks.
    /// </summary>
    internal Task<TResult> ExecuteAsync<TResult>(FfMpegTaskBase<TResult> task)
    {
      var arguments = task.CreateArguments();
      var ffProcess = this._processFactory.LaunchFfMpeg(arguments);

      return task.ExecuteCommandAsync(ffProcess);
    }
  }
}
