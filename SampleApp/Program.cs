using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Services;
using MediaToolkit.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SampleApp
{
  class Program
  {
    static async Task Main(string[] args)
    {
      // Note: this sample assumes that under Windows we run the app with Visual Studio.
      // Under Linux we use vscode
      string ffmpegFilePath = @"C:\ffmpeg\ffmpeg.exe";
      string ffprobeFilePath = null;
      var videoPath = Path.GetFullPath(@"..\..\..\..\MediaToolkit.Test\TestVideo\BigBunny.m4v");
      var thumbnailPath = Path.GetFullPath(@"..\..\..\..\MediaToolkit.Test\TestVideo\thumbnail.jpeg");

      if(!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        ffmpegFilePath = @"/usr/bin/ffmpeg";
        ffprobeFilePath = @"/usr/bin/ffprobe";
        videoPath = Path.GetFullPath(@"../MediaToolkit.Test/TestVideo/BigBunny.m4v");
        thumbnailPath = Path.GetFullPath(@"../MediaToolkit.Test/TestVideo/thumbnail.jpeg");
      }

      var serviceProvider = new ServiceCollection()
        .AddMediaToolkit(ffmpegFilePath, ffprobeFilePath)
        .BuildServiceProvider();

      // Get metadata
      var service = serviceProvider.GetService<IMediaToolkitService>();
      var metadataTask = new FfTaskGetMetadata(videoPath);
      var metadataResult = await service.ExecuteAsync(metadataTask);

      Console.WriteLine("Get metadata: \n");
      Console.Write(JsonSerializer.Serialize(metadataResult.Metadata));
      Console.WriteLine("\n");

      Console.WriteLine("Save thumbnail: \n");
      var saveThumbnailTask = new FfTaskSaveThumbnail(videoPath, thumbnailPath, TimeSpan.FromSeconds(10));
      await service.ExecuteAsync(saveThumbnailTask);
    }
  }
}
