using System.IO;
using System.Threading.Tasks;
using Medallion.Shell.Streams;

namespace MediaToolkit.Core
{
  /// <summary>
  /// Provides stream reader interfaces
  /// </summary>
  internal class StreamReaderWrapper : IProcessStreamReader
  {
    private ProcessStreamReader _streamReader;

    /// <summary>
    /// Ctor.
    /// </summary>
    public StreamReaderWrapper(ProcessStreamReader streamReader)
    {
      this._streamReader = streamReader;
    }

    /// <summary>
    /// IProcessStreamReader
    /// </summary>
    public Stream BaseStream => this._streamReader.BaseStream;

    /// <summary>
    /// IProcessStreamReader
    /// </summary>
    public Task<string> ReadToEndAsync()
    {
      return this._streamReader.ReadToEndAsync();
    }
  }
}
