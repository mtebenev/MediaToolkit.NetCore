using System;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Threading;
using MediaToolkit.Properties;
using MediaToolkit.Util;

namespace MediaToolkit
{
  public class EngineBase : IDisposable
  {
    private bool isDisposed;

    /// <summary>   Used for locking the FFmpeg process to one thread. </summary>
    private const string LockName = "MediaToolkit.Engine.LockName";

    private readonly string _ffprobeFilePath;

    private readonly IFileSystem _fileSystem;

    /// <summary>   The Mutex. </summary>
    protected readonly Mutex Mutex;

    /// <summary>   The ffmpeg process. </summary>
    protected Process FFmpegProcess;

    /// <summary>
    /// Initializes FFmpeg.exe; Ensuring that there is a copy in the clients temp folder &amp; isn't in use by another process.
    /// Assumes that ffprobe located in the same directory as ffmpeg
    /// </summary>
    protected EngineBase(string ffMpegPath, IFileSystem fileSystem)
    {
      _fileSystem = fileSystem;
      Mutex = new Mutex(false, LockName);
      isDisposed = false;

      if(ffMpegPath.IsNullOrWhiteSpace())
        throw new ArgumentException(nameof(ffMpegPath));

      FfmpegFilePath = ffMpegPath;
      var ffmpegDirectoryPath = _fileSystem.FileInfo.FromFileName(ffMpegPath).DirectoryName;
      FfprobeFilePath = _fileSystem.Path.Combine(ffmpegDirectoryPath, "ffprobe.exe");

      EnsureFFmpegFileExists();
      EnsureFFmpegIsNotUsed();
    }

    public string FfmpegFilePath { get; }
    public string FfprobeFilePath { get; }

    private void EnsureFFmpegIsNotUsed()
    {
      try
      {
        this.Mutex.WaitOne();
        Process.GetProcessesByName(Resources.FFmpegProcessName)
            .ForEach(process =>
            {
              process.Kill();
              process.WaitForExit();
            });
      }
      finally
      {
        this.Mutex.ReleaseMutex();
      }
    }

    private void EnsureFFmpegFileExists()
    {
      if(!_fileSystem.File.Exists(FfmpegFilePath))
        throw new InvalidOperationException("Unable to locate ffmpeg executable. Make sure it exists at path passed to Engine constructor");

      if(!_fileSystem.File.Exists(FfprobeFilePath))
        throw new InvalidOperationException("Unable to locate ffprobe executable. Make sure it exists at path passed to Engine constructor");
    }


    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting
    ///     unmanaged resources.
    /// </summary>
    /// <remarks>   Aydin Aydin, 30/03/2015. </remarks>
    public virtual void Dispose()
    {
      this.Dispose(true);
    }

    private void Dispose(bool disposing)
    {
      if(!disposing || this.isDisposed)
      {
        return;
      }

      if(FFmpegProcess != null)
      {
        this.FFmpegProcess.Dispose();
      }

      this.FFmpegProcess = null;
      this.isDisposed = true;
    }
  }
}
