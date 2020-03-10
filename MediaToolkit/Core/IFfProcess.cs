using System.Threading.Tasks;

namespace MediaToolkit.Core
{
  /// <summary>
  /// The interface for executed FF tool process.
  /// </summary>
  public interface IFfProcess
  {
    /// <summary>
    /// Awaits for the process and returns standard output as string.
    /// </summary>
    Task<FfTaskResult> Run();
  }
}
