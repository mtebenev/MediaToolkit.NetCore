using System.IO;
using System.Threading.Tasks;

namespace MediaToolkit.Core
{
  /// <summary>
  /// The process stream reader interface.
  /// </summary>
  public interface IProcessStreamReader
  {
    /// <summary>
    /// The underlying stream.
    /// </summary>
    Stream BaseStream { get; }

    /// <summary>
    /// Reads all characters from the current position to the end of the text reader
    /// asynchronously and returns them as one string.
    /// </summary>
    Task<string> ReadToEndAsync();
  }
}
