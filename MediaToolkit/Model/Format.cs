namespace MediaToolkit.Model
{
  public class Format
  {
    public string Filename { get; set; }
    public int NbStreams { get; set; }
    public int NbPrograms { get; set; }
    public string FormatName { get; set; }
    public string FormatLongName { get; set; }
    public string StartTime { get; set; }
    public string Duration { get; set; }
    public string Size { get; set; }
    public string BitRate { get; set; }
    public int ProbeScore { get; set; }
    public FormatTags Tags { get; set; }
  }
}
