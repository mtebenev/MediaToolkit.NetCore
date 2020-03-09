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

    public static IFfService CreateFfService()
    {
      var ffProcessFactory = new FfProcessFactory();
      var result = new FfService(ffProcessFactory);
      return result;
    }

    public Task ExecuteAsync(FfTaskBase task)
    {
      var result = task.ExecuteAsync(this);
      return result;
    }

    /// <summary>
    /// Dispatcher for ffprobe tasks.
    /// </summary>
    internal Task ExecuteAsync(FfProbeTaskBase task)
    {
      var arguments = task.CreateArguments();
      var ffProcess = this._processFactory.LaunchFfProbe(arguments);

      return task.ExecuteCommandAsync(ffProcess);
    }

    /// <summary>
    /// Dispatcher for ffmpeg tasks.
    /// </summary>
    internal Task ExecuteAsync(FfMpegTaskBase task)
    {
      return Task.CompletedTask;
    }
  }
}
