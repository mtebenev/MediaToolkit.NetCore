using System.Threading.Tasks;
using MediaToolkit.Core;
using MediaToolkit.Services;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// Common interface for FF tasks.
  /// </summary>
  public abstract class FfTaskBase
  {
    /// <summary>
    /// Override to create the call parameters.
    /// </summary>
    public abstract string[] CreateArguments();

    /// <summary>
    /// Override to execute the command.
    /// </summary>
    public abstract Task ExecuteCommandAsync(IFfProcess ffProcess);

    /// <summary>
    /// Internal method for execution with the service instance.
    /// </summary>
    internal abstract Task ExecuteAsync(FfService ffService);
  }
}
