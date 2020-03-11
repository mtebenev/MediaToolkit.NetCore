using System;
using System.IO;
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
      var serviceProvider = new ServiceCollection()
        .AddMediaToolkit()
        .BuildServiceProvider();

      var videoPath = Path.GetFullPath(@"..\..\..\..\MediaToolkit.Test\TestVideo\BigBunny.m4v");

      // Get metadata
      var service = serviceProvider.GetService<IMediaToolkitService>();
      var metadataTask = new FfTaskGetMetadata(videoPath);
      var metadataResult = await service.ExecuteAsync(metadataTask);

      Console.WriteLine("Get metadata: \n");
      Console.Write(JsonSerializer.Serialize(metadataResult.Metadata));
      Console.WriteLine("\n");

      Console.WriteLine("Save thumbnail: \n");
      var thumbnailPath = Path.GetFullPath(@"..\..\..\..\MediaToolkit.Test\TestVideo\thumbnail.jpeg");
      var saveThumbnailTask = new FfTaskSaveThumbnail(videoPath, thumbnailPath, TimeSpan.FromSeconds(10));
      await service.ExecuteAsync(saveThumbnailTask);
    }
  }
}
