using System.Threading.Tasks;
using Medallion.Shell.Streams;

namespace MediaToolkit.Core
{
  /// <summary>
  /// The interface for executed FF tool process.
  /// </summary>
  public interface IFfProcess
  {
    /// <summary>
    /// The task awaiting the process complete.
    /// </summary>
    Task Task { get; }

    /// <summary>
    /// The standard output.
    /// </summary>
    IProcessStreamReader OutputReader { get; }

    /// <summary>
    /// The standard error.
    /// </summary>
    IProcessStreamReader ErrorReader { get; }
  }
}
