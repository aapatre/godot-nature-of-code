using Godot;
using Forces;

namespace Examples
{
  namespace Chapter2
  {
    /// <summary>
    /// Example 2.6 - Attraction.
    /// </summary>
    /// Uses SimpleAttractor to automatically attract SimpleMovers.
    public class C2Example6 : Node2D, IExample
    {
      public string _Summary()
      {
        return "Example 2.6:\n"
          + "Attraction";
      }

      public override void _Ready()
      {
        var size = GetViewportRect().Size;

        var attractor = new SimpleAttractor();
        attractor.Gravitation = 0.5f;
        attractor.Position = size / 2;
        AddChild(attractor);

        var mover = new SimpleMover(SimpleMover.WrapModeEnum.Bounce);
        var xPos = (float)GD.RandRange(mover.Radius, size.x - mover.Radius);
        var yPos = (float)GD.RandRange(mover.Radius, size.y - mover.Radius);
        mover.Position = new Vector2(xPos, yPos);
        AddChild(mover);
      }
    }
  }
}
