using Godot;

namespace Drawing
{
  /// <summary>
  /// Simple trail drawing.
  /// </summary>
  public class SimpleTrail : Line2D
  {
    /// <summary>Point count</summary>
    public int PointCount = 50;

    /// <summary>Trail color</summary>
    public Color TrailColor = Colors.Purple;

    /// <summary>Target node</summary>
    public Node2D Target;

    /// <summary>
    /// Create a simple purple trail with a 2.5px width.
    /// </summary>
    public SimpleTrail()
    {
      Width = 2.5f;
    }

    public override void _Ready()
    {
      // Gradient
      var gradient = new Gradient();
      gradient.SetColor(0, TrailColor.WithAlpha(0));
      gradient.AddPoint(1, TrailColor);
      gradient.SetOffset(0, 0);
      gradient.SetOffset(1, 1);

      Gradient = gradient;
    }

    public override void _Process(float delta)
    {
      AddPoint(Target.GlobalPosition);
      while (GetPointCount() > PointCount)
      {
        RemovePoint(0);
      }
    }
  }
}
