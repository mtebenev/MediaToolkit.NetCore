using System.IO;

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
  }
}
