using System;
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
    public async Task<FfTaskResult> Run()
    {
      var command = Command.Run(this._ffToolFilePath, this._arguments);

      var commandResult = await command.Task;
      var output = await command.StandardOutput.ReadToEndAsync();
      var error = await command.StandardError.ReadToEndAsync();

      if(!commandResult.Success)
      {
        throw new InvalidOperationException(error);
      }
      
      return new FfTaskResult(output, error);
    }
  }
}
