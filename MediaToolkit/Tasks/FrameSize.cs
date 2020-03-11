namespace MediaToolkit.Tasks
{
  /// <summary>
  /// Describes the width/height of the video frame.
  /// </summary>
  public class FrameSize
  {
    public FrameSize(int width, int height)
    {
      this.Width = width;
      this.Height = height;
    }

    public int Width { get; }
    public int Height { get; }

    public override string ToString()
    {
      return $"{this.Width}x{this.Height}";
    }
  }
}
