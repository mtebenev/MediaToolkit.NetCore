using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using MediaToolkit.Core;
using MediaToolkit.Services;
using Xunit;

namespace MediaToolkit.Test.Core
{
  public class FfProcessFactoryTest
  {
    [Fact]
    public void Should_Throw_If_Ffmpeg_Not_Found()
    {
      var options = new MediaToolkitOptions { FfMpegPath = @"c:\non_existing\ffmpeg.exe" };
      var mockFs = new MockFileSystem();
      Assert.Throws<InvalidOperationException>(() => new FfProcessFactory(options, mockFs));
    }

    [Fact]
    public void Should_Throw_If_Ffprobe_Not_Found()
    {
      var options = new MediaToolkitOptions { FfMpegPath = @"c:\existing\ffmpeg.exe" };
      var mockFs = new MockFileSystem(
        new Dictionary<string, MockFileData>
        {
          { @"c:\existing\ffmpeg.exe", new MockFileData("abc") }
        });

      Assert.Throws<InvalidOperationException>(() => new FfProcessFactory(options, mockFs));
    }
  }
}
