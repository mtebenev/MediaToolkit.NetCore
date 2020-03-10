using System.IO;
using Medallion.Shell.Streams;

namespace MediaToolkit.Core
{
  /// <summary>
  /// Provides stream reader interfaces
  /// </summary>
  internal class StreamReaderWrapper : IProcessStreamReader
  {
    private ProcessStreamReader _streamReader;

    public StreamReaderWrapper(ProcessStreamReader streamReader)
    {
      this._streamReader = streamReader;
    }

    public Stream BaseStream => this._streamReader.BaseStream;
  }
}
