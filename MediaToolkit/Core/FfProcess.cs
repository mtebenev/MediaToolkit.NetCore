using System.Threading.Tasks;
using Medallion.Shell;

namespace MediaToolkit.Core
{
  /// <summary>
  /// FF process implementation
  /// </summary>
  internal class FfProcess : IFfProcess
  {
    private string _ffToolFilePath;
    private string[] _arguments;

    /// <summary>
    /// Ctor.
    /// </summary>
    public FfProcess(string ffprobeFilePath, string[] arguments)
    {
      this._ffToolFilePath = ffprobeFilePath;
      this._arguments = arguments;
    }

    /// <summary>
    /// IFfProcess
    /// </summary>
    public async Task<string> Run()
    {
      var cmd = Command.Run(this._ffToolFilePath, this._arguments);
      await cmd.Task;
      var output = await cmd.StandardOutput.ReadToEndAsync();

      return output;
    }
  }
}
