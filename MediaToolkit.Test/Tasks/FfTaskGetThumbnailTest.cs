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
      var task = new FfTaskGetThumbnail(@"c:\some\path", TimeSpan.FromSeconds(15), new FrameSize(100, 200));
      var arguments = task.CreateArguments();

      Assert.Equal(@"-hide_banner -loglevel info -ss 15 -i c:\some\path -t 1 -f gif -vframes 1 -s 100x200 -", String.Join(' ', arguments));
    }

    [Fact]
    public void Should_Use_Default_Frame_Size()
    {
      var task = new FfTaskGetThumbnail(@"c:\some\path", TimeSpan.FromSeconds(15), null);
      var arguments = task.CreateArguments();

      Assert.Equal(@"-hide_banner -loglevel info -ss 15 -i c:\some\path -t 1 -f gif -vframes 1 -", String.Join(' ', arguments));
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
        var task = new FfTaskGetThumbnail(@"c:\some\path", TimeSpan.FromSeconds(15), null);
        result = await task.ExecuteCommandAsync(mockFfProcess);
      }

      Assert.Equal(new byte[] { 10, 20, 30 }, result.ThumbnailData);
    }
  }
}
