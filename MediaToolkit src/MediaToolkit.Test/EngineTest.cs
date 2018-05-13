using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace MediaToolkit.Test
{
    public class EngineTest
    {
        /// <summary>
        /// Path to ffprobe.exe should be composed using the same directory as ffmpeg
        /// </summary>
        [Fact]
        public void Should_Initialize_FFprobe_Path()
        {
            // Create SUT directly intentionally
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"c:\some\folder\path\ffmpeg.exe", new MockFileData("")},
                {@"c:\some\folder\path\ffprobe.exe", new MockFileData("")}
            });

            var engine = new Engine(@"c:\some\folder\path\ffmpeg.exe", fileSystem);

            Assert.Equal(@"c:\some\folder\path\ffmpeg.exe", engine.FfmpegFilePath);
            Assert.Equal(@"c:\some\folder\path\ffprobe.exe", engine.FfprobeFilePath);
        }
    }
}
