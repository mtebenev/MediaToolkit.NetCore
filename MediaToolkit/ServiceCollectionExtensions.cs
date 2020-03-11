using System;
using System.IO.Abstractions;
using MediaToolkit.Core;
using MediaToolkit.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediaToolkit
{
  public static class ServiceCollectionExtensions
  {
    /// <summary>
    /// Adds the MediaToolkit to the service collection.
    /// </summary>
    public static IServiceCollection AddMediaToolkit(this IServiceCollection services, string ffmpegFilePath)
    {
      if(services == null)
      {
        throw new ArgumentNullException(nameof(services));
      }

      var options = new MediaToolkitOptions
      {
        FfMpegPath = ffmpegFilePath
      };

      services.TryAddSingleton<IFileSystem, FileSystem>();
      services.AddSingleton(options);
      services.AddSingleton<IffProcessFactory, FfProcessFactory>();
      services.AddSingleton<IMediaToolkitService, MediaToolkitService>();

      return services;
    }
  }
}
