using MediaToolkit.Core;
using MediaToolkit.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MediaToolkit
{
  public static class ServiceCollectionExtensions
  {
    /// <summary>
    /// Adds the MediaToolkit to the service collection.
    /// </summary>
    public static IServiceCollection AddMediaToolkit(this IServiceCollection services)
    {
      services.AddSingleton<IffProcessFactory, FfProcessFactory>();
      services.AddSingleton<IMediaToolkitService, MediaToolkitService>();

      return services;
    }
  }
}
