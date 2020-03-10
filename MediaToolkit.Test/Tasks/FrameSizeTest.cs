using MediaToolkit.Tasks;
using Xunit;

namespace MediaToolkit.Test.Tasks
{
  public class FrameSizeTest
  {
    [Fact]
    public void Should_Produce_Ffmpeg_Size()
    {
      var frameSize = new FrameSize(100, 100);
      Assert.Equal("100x100", frameSize.ToString());
    }
  }
}
