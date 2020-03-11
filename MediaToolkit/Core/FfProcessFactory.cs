using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using MediaToolkit.Services;

namespace MediaToolkit.Core
{
  /// <summary>
  /// The process factory implementation.
  /// </summary>
  internal class FfProcessFactory : IffProcessFactory
  {
    private readonly string _ffprobeFilePath;
    private readonly string _ffmpegFilePath;

    public FfProcessFactory(MediaToolkitOptions options, IFileSystem fileSystem)
    {
      if(options == null || string.IsNullOrEmpty(options.FfMpegPath))
      {
        throw new ArgumentNullException(nameof(options.FfMpegPath));
      }

      this._ffmpegFilePath = options.FfMpegPath;
      var ffmpegDirectoryPath = fileSystem.FileInfo.FromFileName(options.FfMpegPath).DirectoryName;
      this._ffprobeFilePath = fileSystem.Path.Combine(ffmpegDirectoryPath, "ffprobe.exe");

      EnsureFFmpegFileExists(fileSystem);
    }

    public IFfProcess LaunchFfMpeg(IEnumerable<string> arguments)
    {
      IFfProcess ffProcess = new FfProcess(this._ffmpegFilePath, arguments);
      return ffProcess;
    }

    public IFfProcess LaunchFfProbe(IEnumerable<string> arguments)
    {
      IFfProcess ffProcess = new FfProcess(this._ffprobeFilePath, arguments);
      return ffProcess;
    }

    private void EnsureFFmpegFileExists(IFileSystem fileSystem)
    {
      if(!fileSystem.File.Exists(this._ffmpegFilePath))
        throw new InvalidOperationException("Unable to locate ffmpeg executable. Make sure it exists at path passed to the MediaToolkit");

      if(!fileSystem.File.Exists(this._ffprobeFilePath))
        throw new InvalidOperationException("Unable to locate ffprobe executable. Make sure it exists at path passed to the MediaToolkit");
    }
  }
}
