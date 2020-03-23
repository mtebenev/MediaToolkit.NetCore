using System;
using System.IO;
using System.Threading.Tasks;
using MediaToolkit.Core;
using MediaToolkit.Tasks;
using NSubstitute;
using Xunit;

namespace MediaToolkit.Test.Tasks
{
  public class FfTaskGetThumbnailTest
  {
    [Fact]
    public void Should_Add_Frame_Size_Arguments()
    {
      var options = new GetThumbnailOptions
      {
        FrameSize = new FrameSize(100, 200),
        SeekSpan = TimeSpan.FromSeconds(15)
      };

      var task = new FfTaskGetThumbnail(@"c:\some\path", options);
      var arguments = task.CreateArguments();

      Assert.Contains("-s 100x200", String.Join(' ', arguments));
    }

    [Fact]
    public void Should_Add_Output_format_Arguments()
    {
      var options = new GetThumbnailOptions
      {
        OutputFormat = OutputFormat.Gif
      };

      var task = new FfTaskGetThumbnail(@"c:\some\path", options);
      var arguments = task.CreateArguments();

      Assert.Contains("-f gif", String.Join(' ', arguments));
    }

    [Fact]
    public void Should_Add_Pixel_format_Arguments()
    {
      var options = new GetThumbnailOptions
      {
        PixelFormat = PixelFormat.Gray
      };

      var task = new FfTaskGetThumbnail(@"c:\some\path", options);
      var arguments = task.CreateArguments();

      Assert.Contains("-pix_fmt gray", String.Join(' ', arguments));
    }

    [Fact]
    public void Should_Generate_Default_Arguments()
    {
      var task = new FfTaskGetThumbnail(@"c:\some\path", new GetThumbnailOptions());
      var arguments = task.CreateArguments();

      var args = String.Join(' ', arguments);
      Assert.Equal(@"-hide_banner -loglevel info -ss 0 -i c:\some\path -t 1 -f rawvideo -vframes 1 -", String.Join(' ', arguments));
    }

    [Fact]
    public async Task Should_Return_Output_As_Byte_Array()
    {
      var mockOutputReader = Substitute.For<IProcessStreamReader>();
      var mockFfProcess = Substitute.For<IFfProcess>();
      mockFfProcess.OutputReader.Returns(mockOutputReader);

      GetThumbnailResult result;
      using(var ms = new MemoryStream(new byte[] { 10, 20, 30 }))
      {
        mockOutputReader.BaseStream.Returns(ms);
        var options = new GetThumbnailOptions
        {
          SeekSpan = TimeSpan.FromSeconds(15)
        };
        var task = new FfTaskGetThumbnail(@"c:\some\path", options);
        result = await task.ExecuteCommandAsync(mockFfProcess);
      }

      Assert.Equal(new byte[] { 10, 20, 30 }, result.ThumbnailData);
    }
  }
}
