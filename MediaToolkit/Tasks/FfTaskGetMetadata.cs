using System.Text.Json;
using System.Threading.Tasks;
using MediaToolkit.Core;
using MediaToolkit.Model;

namespace MediaToolkit.Tasks
{
  /// <summary>
  /// The task retrieves the file metadata using ffprobe.
  /// </summary>
  public class FfTaskGetMetadata : FfProbeTaskBase<GetMetadataResult>
  {
    private readonly string _filePath;

    public FfTaskGetMetadata(string filePath)
    {
      this._filePath = filePath;
    }

    public override string[] CreateArguments()
    {
      var arguments = new[]
      {
        "-v",
        "quiet",
        "-print_format",
        "json",
        "-show_format",
        "-show_streams",
        this._filePath
      };
      return arguments;
    }

    public override async Task<GetMetadataResult> ExecuteCommandAsync(IFfProcess ffProcess)
    {
      var taskResult = await ffProcess.Run();

      var options = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };
      var ffProbeOutput = JsonSerializer.Deserialize<FfProbeOutput>(taskResult.Output, options);
      var result = new GetMetadataResult(ffProbeOutput);
      return result;
    }
  }
}
