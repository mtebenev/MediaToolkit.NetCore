using System.Text.Json.Serialization;

namespace MediaToolkit.Model
{
  public class MediaStream
  {
    public int Index { get; set; }

    [JsonPropertyName("codec_name")]
    public string CodecName { get; set; }

    [JsonPropertyName("codec_long_name")]
    public string CodecLongName { get; set; }
    public string Profile { get; set; }

    [JsonPropertyName("codec_type")]
    public string CodecType { get; set; }

    [JsonPropertyName("codec_time_base")]
    public string CodecTimeBase { get; set; }

    [JsonPropertyName("codec_tag_string")]
    public string CodecTagString { get; set; }

    [JsonPropertyName("codec_tag")]
    public string CodecTag { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    [JsonPropertyName("coded_width")]
    public int CodedWidth { get; set; }

    [JsonPropertyName("coded_height")]
    public int CodedHeight { get; set; }

    [JsonPropertyName("has_b_frames")]
    public int HasBFrames { get; set; }

    [JsonPropertyName("sample_aspect_ratio")]
    public string SampleAspectRatio { get; set; }

    [JsonPropertyName("display_aspect_ratio")]
    public string DisplayAspectRatio { get; set; }

    [JsonPropertyName("pix_fmt")]
    public string PixFmt { get; set; }

    public int Level { get; set; }

    [JsonPropertyName("chroma_location")]
    public string ChromaLocation { get; set; }

    public int Refs { get; set; }

    [JsonPropertyName("is_avc")]
    public string IsAvc { get; set; }

    [JsonPropertyName("nal_length_size")]
    public string NalLengthSize { get; set; }

    [JsonPropertyName("r_frame_rate")]
    public string RFrameRate { get; set; }

    [JsonPropertyName("avg_frame_rate")]
    public string AvgFrameRate { get; set; }

    [JsonPropertyName("time_base")]
    public string TimeBase { get; set; }

    [JsonPropertyName("start_pts")]
    public int StartPts { get; set; }

    [JsonPropertyName("start_time")]
    public string StartTime { get; set; }

    [JsonPropertyName("duration_ts")]
    public int DurationTs { get; set; }
    public string Duration { get; set; }

    [JsonPropertyName("bit_rate")]
    public string BitRate { get; set; }

    [JsonPropertyName("bits_per_raw_sample")]
    public string BitsPerRawSample { get; set; }

    [JsonPropertyName("nb_frames")]
    public string NbFrames { get; set; }

    public Disposition Disposition { get; set; }
    public StreamTags Tags { get; set; }

    [JsonPropertyName("sample_fmt")]
    public string SampleFmt { get; set; }

    [JsonPropertyName("sample_rate")]
    public string SampleRate { get; set; }

    public int? Channels { get; set; }

    [JsonPropertyName("channel_layout")]
    public string ChannelLayout { get; set; }

    [JsonPropertyName("bits_per_sample")]
    public int? BitsPerSample { get; set; }

    [JsonPropertyName("max_bit_rate")]
    public string MaxBitRate { get; set; }

    [JsonPropertyName("color_range")]
    public string ColorRange { get; set; }

    [JsonPropertyName("color_space")]
    public string ColorSpace { get; set; }

    [JsonPropertyName("color_transfer")]
    public string ColorTransfer { get; set; }

    [JsonPropertyName("color_primaries")]
    public string ColorPrimaries { get; set; }
  }
}
