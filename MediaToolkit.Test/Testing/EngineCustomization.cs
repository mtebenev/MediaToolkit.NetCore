using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using AutoFixture;

namespace MediaToolkit.Test.Testing
{
  /// <summary>
  /// Abstracts engine creation
  /// </summary>
  internal class EngineCustomization : ICustomization
  {
    public void Customize(IFixture fixture)
    {
      fixture.Customize<Engine>(c =>
          c.FromFactory(() =>
          {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
              {
                        {@"c:\ffmpeg\ffmpeg.exe", new  MockFileData("")},
                        {@"c:\ffmpeg\ffprobe.exe", new MockFileData(new byte[] {0x12, 0x34, 0x56, 0xd2})}
              });
            return new Engine(@"C:\ffmpeg\FFmpeg.exe", fileSystem);
          }));
    }
  }
}
