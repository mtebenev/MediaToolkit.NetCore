using System.Collections.Generic;
using System.Threading.Tasks;
using MediaToolkit.Core;
using MediaToolkit.Tasks;
using NSubstitute;
using Xunit;

namespace MediaToolkit.Test.Tasks
{
  public class FfTaskGetMetadataTest
  {
    [Fact]
    public async Task Should_Deserialize_Format()
    {
      var mockOutputReader = Substitute.For<IProcessStreamReader>();
      var mockFfProcess = Substitute.For<IFfProcess>();
      mockFfProcess.OutputReader.Returns(mockOutputReader);

      var json =
@"{
  'format': {
    'filename': 'C:\\folder\\video.wmv',
    'nb_streams': 2,
    'nb_programs': 3,
    'format_name': 'asf',
    'format_long_name': 'ASF (Advanced / Active Streaming Format)',
    'start_time': '0.000000',
    'duration': '47.360000',
    'size': '9964791',
    'bit_rate': '1683241',
    'probe_score': 100
  }
}";
      mockOutputReader.ReadToEndAsync().Returns(json.Replace('\'', '\"'));
      var task = new FfTaskGetMetadata(@"som_path");

      var result = await task.ExecuteCommandAsync(mockFfProcess);

      // Verify
      var format = result.Metadata.Format;
      Assert.Equal(@"C:\folder\video.wmv", format.Filename);
      Assert.Equal(2, format.NbStreams);
      Assert.Equal(3, format.NbPrograms);
      Assert.Equal("asf", format.FormatName);
      Assert.Equal("ASF (Advanced / Active Streaming Format)", format.FormatLongName);
      Assert.Equal("0.000000", format.StartTime); // TODO: timespan
      Assert.Equal("47.360000", format.Duration); // TODO: timespan
      Assert.Equal("9964791", format.Size); // TOOD: long
      Assert.Equal("1683241", format.BitRate); // TOOD: int
      Assert.Equal(100, format.ProbeScore); // TOOD: int
    }

    [Fact]
    public async Task Should_Deserialize_Format_Tags()
    {
      var mockOutputReader = Substitute.For<IProcessStreamReader>();
      var mockFfProcess = Substitute.For<IFfProcess>();
      mockFfProcess.OutputReader.Returns(mockOutputReader);

      var json =
@"{
  'format': {
    'tags': {
      'Application': 'Windows Movie Maker 6.0.6000.16386',
      'WM/ToolVersion': '6.0.6000.16386',
      'WM/ToolName': 'Windows Movie Maker',
      'WMFSDKVersion': '11.0.6000.6346',
      'WMFSDKNeeded': '0.0.0.0000',
      'Buffer Average': '2688',
      'VBR Peak': '1459825',
      'IsVBR': '1',
      'DeviceConformanceTemplate': 'MP@HL'
    }
  }
}";
      mockOutputReader.ReadToEndAsync().Returns(json.Replace('\'', '\"'));
      var task = new FfTaskGetMetadata(@"som_path");

      var result = await task.ExecuteCommandAsync(mockFfProcess);

      // Verify
      var expectedTags = new Dictionary<string, string>
      {
        { "Application", "Windows Movie Maker 6.0.6000.16386"},
        { "WM/ToolVersion", "6.0.6000.16386"},
        { "WM/ToolName", "Windows Movie Maker"},
        { "WMFSDKVersion", "11.0.6000.6346"},
        { "WMFSDKNeeded", "0.0.0.0000"},
        { "Buffer Average", "2688"},
        { "VBR Peak", "1459825"},
        { "IsVBR", "1"},
        { "DeviceConformanceTemplate", "MP@HL"}
      };

      Assert.Equal(expectedTags, result.Metadata.Format.Tags);
    }

    [Fact]
    public async Task Should_Deserialize_Video_Stream()
    {
      var mockOutputReader = Substitute.For<IProcessStreamReader>();
      var mockFfProcess = Substitute.For<IFfProcess>();
      mockFfProcess.OutputReader.Returns(mockOutputReader);

      var json =
@"{
  'streams': [
    {
      'index': 3,
      'codec_name': 'h264',
      'codec_long_name': 'H.264 / AVC / MPEG-4 AVC / MPEG-4 part 10',
      'profile': 'High',
      'codec_type': 'video',
      'codec_time_base': '21285403/1064160000',
      'codec_tag_string': 'avc1',
      'codec_tag': '0x31637661',
      'width': 1280,
      'height': 720,
      'coded_width': 1280,
      'coded_height': 720,
      'has_b_frames': 2,
      'sample_aspect_ratio': '1:1',
      'display_aspect_ratio': '16:9',
      'pix_fmt': 'yuv420p',
      'level': 31,
      'color_range': 'tv',
      'color_space': 'bt709',
      'color_transfer': 'bt709',
      'color_primaries': 'bt709',
      'chroma_location': 'left',
      'refs': 1,
      'is_avc': 'true',
      'nal_length_size': '4',
      'r_frame_rate': '25/1',
      'avg_frame_rate': '532080000/21285403',
      'time_base': '1/90000',
      'start_pts': 11520,
      'start_time': '0.128000',
      'duration_ts': 85141612,
      'duration': '946.017911',
      'bit_rate': '1209472',
      'bits_per_raw_sample': '8',
      'nb_frames': '23648'
    }
  ]
}";
      mockOutputReader.ReadToEndAsync().Returns(json.Replace('\'', '\"'));
      var task = new FfTaskGetMetadata(@"som_path");

      var result = await task.ExecuteCommandAsync(mockFfProcess);

      // Verify
      var stream = result.Metadata.Streams[0];
      Assert.Equal(3, stream.Index);
      Assert.Equal("h264", stream.CodecName);
      Assert.Equal("H.264 / AVC / MPEG-4 AVC / MPEG-4 part 10", stream.CodecLongName);
      Assert.Equal("High", stream.Profile);
      Assert.Equal("video", stream.CodecType);
      Assert.Equal("21285403/1064160000", stream.CodecTimeBase);
      Assert.Equal("avc1", stream.CodecTagString);
      Assert.Equal("0x31637661", stream.CodecTag);
      Assert.Equal(1280, stream.Width);
      Assert.Equal(720, stream.Height);
      Assert.Equal(1280, stream.CodedWidth);
      Assert.Equal(720, stream.CodedHeight);
      Assert.Equal(2, stream.HasBFrames);
      Assert.Equal("1:1", stream.SampleAspectRatio);
      Assert.Equal("16:9", stream.DisplayAspectRatio);
      Assert.Equal("yuv420p", stream.PixFmt);
      Assert.Equal(31, stream.Level);
      Assert.Equal("tv", stream.ColorRange);
      Assert.Equal("bt709", stream.ColorSpace);
      Assert.Equal("bt709", stream.ColorTransfer);
      Assert.Equal("bt709", stream.ColorPrimaries);
      Assert.Equal("left", stream.ChromaLocation);
      Assert.Equal(1, stream.Refs);
      Assert.Equal("true", stream.IsAvc);
      Assert.Equal("4", stream.NalLengthSize);
      Assert.Equal("25/1", stream.RFrameRate);
      Assert.Equal("532080000/21285403", stream.AvgFrameRate);
      Assert.Equal("1/90000", stream.TimeBase);
      Assert.Equal(11520, stream.StartPts);
      Assert.Equal("0.128000", stream.StartTime);
      Assert.Equal(85141612, stream.DurationTs);
      Assert.Equal("946.017911", stream.Duration);
      Assert.Equal("1209472", stream.BitRate);
      Assert.Equal("8", stream.BitsPerRawSample);
      Assert.Equal("23648", stream.NbFrames);
    }

    [Fact]
    public async Task Should_Deserialize_Audio_Stream()
    {
      var mockOutputReader = Substitute.For<IProcessStreamReader>();
      var mockFfProcess = Substitute.For<IFfProcess>();
      mockFfProcess.OutputReader.Returns(mockOutputReader);

      var json =
@"{
  'streams': [
    {
      'sample_fmt': 'fltp',
      'sample_rate': '48000',
      'channels': 2,
      'channel_layout': 'stereo',
      'bits_per_sample': 11,
      'bit_rate': '158337',
      'max_bit_rate': '200000'
    }
  ]
}";
      mockOutputReader.ReadToEndAsync().Returns(json.Replace('\'', '\"'));
      var task = new FfTaskGetMetadata(@"som_path");

      var result = await task.ExecuteCommandAsync(mockFfProcess);

      // Verify
      var stream = result.Metadata.Streams[0];
      Assert.Equal("fltp", stream.SampleFmt);
      Assert.Equal("48000", stream.SampleRate);
      Assert.Equal(2, stream.Channels);
      Assert.Equal("stereo", stream.ChannelLayout);
      Assert.Equal(11, stream.BitsPerSample);
      Assert.Equal("158337", stream.BitRate);
      Assert.Equal("200000", stream.MaxBitRate);
    }

    [Fact]
    public async Task Should_Deserialize_Stream_Disposition()
    {
      var mockOutputReader = Substitute.For<IProcessStreamReader>();
      var mockFfProcess = Substitute.For<IFfProcess>();
      mockFfProcess.OutputReader.Returns(mockOutputReader);

      var json =
@"{
  'streams': [
    {
      'disposition': {
        'default': 1,
        'dub': 2,
        'original': 3,
        'comment': 4,
        'lyrics': 5,
        'karaoke': 6,
        'forced': 7,
        'hearing_impaired': 8,
        'visual_impaired': 9,
        'clean_effects': 2,
        'attached_pic': 3,
        'timed_thumbnails': 4
      }
    }
  ]
}";
      mockOutputReader.ReadToEndAsync().Returns(json.Replace('\'', '\"'));
      var task = new FfTaskGetMetadata(@"som_path");

      var result = await task.ExecuteCommandAsync(mockFfProcess);

      // Verify
      var disposition = result.Metadata.Streams[0].Disposition;
      Assert.Equal(1, disposition.Default);
      Assert.Equal(2, disposition.Dub);
      Assert.Equal(3, disposition.Original);
      Assert.Equal(4, disposition.Comment);
      Assert.Equal(5, disposition.Lyrics);
      Assert.Equal(6, disposition.Karaoke);
      Assert.Equal(7, disposition.Forced);
      Assert.Equal(8, disposition.HearingImpaired);
      Assert.Equal(9, disposition.VisualImpaired);
      Assert.Equal(2, disposition.CleanEffects);
      Assert.Equal(3, disposition.AttachedPic);
      Assert.Equal(4, disposition.TimedThumbnails);
    }

    [Fact]
    public async Task Should_Deserialize_Stream_Tags()
    {
      var mockOutputReader = Substitute.For<IProcessStreamReader>();
      var mockFfProcess = Substitute.For<IFfProcess>();
      mockFfProcess.OutputReader.Returns(mockOutputReader);

      var json =
@"{
  'streams': [
    {
      'tags': {
          'language': 'eng',
          'handler_name': 'Stereo'
      }
    }
  ]
}";
      mockOutputReader.ReadToEndAsync().Returns(json.Replace('\'', '\"'));
      var task = new FfTaskGetMetadata(@"som_path");

      var result = await task.ExecuteCommandAsync(mockFfProcess);

      // Verify
      var expectedTags = new Dictionary<string, string>
      {
        { "language", "eng"},
        { "handler_name", "Stereo"}
      };

      Assert.Equal(expectedTags, result.Metadata.Streams[0].Tags);
    }
  }
}
