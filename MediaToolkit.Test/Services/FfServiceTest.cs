using System.Threading.Tasks;
using MediaToolkit.Core;
using MediaToolkit.Services;
using MediaToolkit.Tasks;
using NSubstitute;
using Xunit;

namespace MediaToolkit.Test.Services
{
  public class FfServiceTest
  {
    [Fact]
    public async Task Should_Start_FfProbe()
    {
      var mockFfProbeTask = Substitute.For<FfProbeTaskBase<int>>();
      var mockProcessFactory = Substitute.For<IffProcessFactory>();
      var mockFfProcess = Substitute.For<IFfProcess>();

      var mockArgs = new[]
      {
        "arg1",
        "arg2"
      };
      mockProcessFactory.LaunchFfProbe(mockArgs).Returns(mockFfProcess);
      mockFfProbeTask.CreateArguments().Returns(mockArgs);

      var service = new MediaToolkitService(mockProcessFactory);
      await service.ExecuteAsync(mockFfProbeTask);

      mockProcessFactory.Received().LaunchFfProbe(mockArgs);
      await mockFfProbeTask.Received().ExecuteCommandAsync(mockFfProcess);
    }

    [Fact]
    public async Task Should_Start_FfMpeg()
    {
      var mockFfMpegTask = Substitute.For<FfMpegTaskBase<int>>();
      var mockProcessFactory = Substitute.For<IffProcessFactory>();
      var mockFfProcess = Substitute.For<IFfProcess>();

      var mockArgs = new[]
      {
        "arg1",
        "arg2"
      };
      mockProcessFactory.LaunchFfMpeg(mockArgs).Returns(mockFfProcess);
      mockFfMpegTask.CreateArguments().Returns(mockArgs);

      var service = new MediaToolkitService(mockProcessFactory);
      await service.ExecuteAsync(mockFfMpegTask);

      mockProcessFactory.Received().LaunchFfMpeg(mockArgs);
      await mockFfMpegTask.Received().ExecuteCommandAsync(mockFfProcess);
    }
  }
}
