using System.Collections.Generic;

namespace MediaToolkit.Model
{
  /// <summary>
  /// DTO for ffprobe output.
  /// </summary>
  public class FfProbeOutput
  {
    public IList<MediaStream> Streams { get; set; }
    public Format Format { get; set; }
  }
}
