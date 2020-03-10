using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MediaToolkit.Model
{
  public class Format
  {
    public string Filename { get; set; }

    [JsonPropertyName("nb_streams")]
    public int NbStreams { get; set; }

    [JsonPropertyName("nb_programs")]
    public int NbPrograms { get; set; }

    [JsonPropertyName("format_name")]
    public string FormatName { get; set; }

    [JsonPropertyName("format_long_name")]
    public string FormatLongName { get; set; }

    [JsonPropertyName("start_time")]
    public string StartTime { get; set; }

    public string Duration { get; set; }
    public string Size { get; set; }

    [JsonPropertyName("bit_rate")]
    public string BitRate { get; set; }

    [JsonPropertyName("probe_score")]
    public int ProbeScore { get; set; }

    public Dictionary<string, string> Tags { get; set; }
  }
}
