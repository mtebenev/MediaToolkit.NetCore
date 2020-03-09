using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Medallion.Shell;
using MediaToolkit.Core;
using MediaToolkit.Model;

namespace MediaToolkit.Tasks
{
  public class FfTaskGetMetadata : FfProbeTaskBase
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
                "xml=fully_qualified=1",
                "-show_format",
                "-show_streams",
                this._filePath
      };
      return arguments;
    }

    public override async Task ExecuteCommandAsync(IFfProcess ffProcess)
    {
      var output = await ffProcess.Run();
      var serializer = new XmlSerializer(typeof(FfprobeType));
      FfprobeType ffprobeType;
      using(var stringReader = new StringReader(output))
      {
        ffprobeType = (FfprobeType)serializer.Deserialize(stringReader);
      }
    }
  }
}
