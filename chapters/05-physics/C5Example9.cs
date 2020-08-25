using Godot;

public class C5Example9 : Node2D, IExample
{
  public string _Summary()
  {
    return "Example 5.9:\n"
      + "Collision Listening\n\n"
      + "Touch screen to spawn balls";
  }

  public class CollisionBall : Physics.SimpleBall
  {
    public bool Colliding = false;

    public CollisionBall()
    {
      ContactMonitor = true;
      ContactsReported = 1;
    }

    public override void _Ready()
    {
      base._Ready();

      Connect("body_entered", this, nameof(OnBodyEntered));
      Connect("body_exited", this, nameof(OnBodyExited));
    }

    public void OnBodyEntered(PhysicsBody2D body)
    {
      Colliding = true;
    }

    public void OnBodyExited(PhysicsBody2D body)
    {
      Colliding = false;
    }

    public override void _Draw()
    {
      // Detect if colliding
      var color = BaseColor;
      if (Colliding)
      {
        color = Colors.Red;
      }

      var outlineVec = new Vector2(OutlineWidth, OutlineWidth);
      DrawCircle(Vector2.Zero, Radius, OutlineColor);
      DrawCircle(Vector2.Zero, Radius - OutlineWidth, color);
    }
  }

  public override void _Ready()
  {
    var size = GetViewportRect().Size;
    var floor = new Physics.SimpleWall();
    floor.BodySize = new Vector2(size.x, 100);
    floor.Position = new Vector2(size.x / 2, size.y);
    AddChild(floor);

    var spawner = new Physics.SimpleTouchSpawner();
    spawner.Spawner = (position) =>
    {
      var ball = new CollisionBall();
      ball.GlobalPosition = position;
      return ball;
    };
    AddChild(spawner);
  }
}