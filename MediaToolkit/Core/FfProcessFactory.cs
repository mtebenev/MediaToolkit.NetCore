namespace MediaToolkit.Core
{
  /// <summary>
  /// The process factory implementation.
  /// </summary>
  internal class FfProcessFactory : IffProcessFactory
  {
    private readonly string _ffprobeFilePath = @"C:\ffmpeg\ffprobe.exe";

    public IFfProcess LaunchFfMpeg(string[] arguments)
    {
      throw new System.NotImplementedException();
    }

    public IFfProcess LaunchFfProbe(string[] arguments)
    {
      IFfProcess ffProcess = new FfProcess(this._ffprobeFilePath, arguments);
      return ffProcess;
    }
  }
}
