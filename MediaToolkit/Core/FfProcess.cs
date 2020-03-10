using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medallion.Shell;
using Medallion.Shell.Streams;

namespace MediaToolkit.Core
{
  /// <summary>
  /// FF process implementation
  /// </summary>
  internal class FfProcess : IFfProcess
  {
    private Command _command;

    /// <summary>
    /// Ctor.
    /// </summary>
    public FfProcess(string ffToolPath, IEnumerable<string> arguments)
    {
      this._command = Command.Run(
        ffToolPath,
        arguments,
        options =>
        {
          options.DisposeOnExit();
        });

      this.Task = Task.Run(async () =>
      {
        var commandResult = await this._command.Task;
        if(!commandResult.Success)
        {
          var error = this._command.StandardError.ReadToEnd();
          throw new InvalidOperationException(error);
        }
      });
    }

    /// <summary>
    /// IFfProcess.
    /// </summary>
    public Task Task { get; }

    /// <summary>
    /// IFfProcess.
    /// </summary>
    public ProcessStreamReader OutputReader => this._command.StandardOutput;

    /// <summary>
    /// IFfProcess.
    /// </summary>
    public ProcessStreamReader ErrorReader => this._command.StandardError;

    /// <summary>
    /// Use to read all the output stream with one call.
    /// </summary>
    public async Task<string> ReadOutputToEndAsync()
    {
      await this.Task;
      var result = await this._command.StandardOutput.ReadToEndAsync();
      return result;
    }

    /// <summary>
    /// Use to read all the error stream with one call.
    /// </summary>
    public async Task<string> ReadErrorToEndAsync()
    {
      await this.Task;
      var result = await this._command.StandardError.ReadToEndAsync();
      return result;
    }
  }
}
