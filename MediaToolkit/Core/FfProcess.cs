using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medallion.Shell;

namespace MediaToolkit.Core
{
  /// <summary>
  /// FF process implementation
  /// </summary>
  internal class FfProcess : IFfProcess
  {
    private Command _command;
    private readonly StreamReaderWrapper _outputReader;
    private readonly StreamReaderWrapper _errorReader;

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
      this._outputReader = new StreamReaderWrapper(this._command.StandardOutput);
      this._errorReader = new StreamReaderWrapper(this._command.StandardError);

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
    public IProcessStreamReader OutputReader => this._outputReader;

    /// <summary>
    /// IFfProcess.
    /// </summary>
    public IProcessStreamReader ErrorReader => this._errorReader;

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
