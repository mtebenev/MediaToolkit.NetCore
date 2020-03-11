using System.Text.Json.Serialization;

namespace MediaToolkit.Model
{
  public class Disposition
  {
    public int Default { get; set; }
    public int Dub { get; set; }
    public int Original { get; set; }
    public int Comment { get; set; }
    public int Lyrics { get; set; }
    public int Karaoke { get; set; }
    public int Forced { get; set; }

    [JsonPropertyName("hearing_impaired")]
    public int HearingImpaired { get; set; }

    [JsonPropertyName("visual_impaired")]
    public int VisualImpaired { get; set; }

    [JsonPropertyName("clean_effects")]
    public int CleanEffects { get; set; }

    [JsonPropertyName("attached_pic")]
    public int AttachedPic { get; set; }

    [JsonPropertyName("timed_thumbnails")]
    public int TimedThumbnails { get; set; }
  }
}
