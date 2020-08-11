using Godot;

public class C1Example11 : Node2D, IExample
{
  public string _Summary()
  {
    return "Example 1.11:\n"
      + "Movers towards mouse";
  }

  public class Mover : SimpleMover
  {
    protected override void UpdateAcceleration()
    {
      var mousePos = GetViewport().GetMousePosition();
      var dir = (mousePos - Position).Normalized();

      Acceleration = dir * 0.5f;
    }
  }

  public override void _Ready()
  {
    GD.Randomize();
    var size = GetViewportRect().Size;

    var moverCount = 20;
    for (int i = 0; i < moverCount; ++i)
    {
      var mover = new Mover();
      mover.Position = new Vector2((float)GD.RandRange(0, size.x), (float)GD.RandRange(0, size.y));
      AddChild(mover);
    }
  }
}
