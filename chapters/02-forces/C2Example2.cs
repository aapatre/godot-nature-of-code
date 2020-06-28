using Godot;
using System.Linq;

public class C2Example2 : Node2D, IExample {
  public string _Summary() {
    return "Example 2.2:\n"
      + "Forces acting on many objects";
  }

  public class Mover : Node2D {
    public Vector2 Velocity = Vector2.Zero;
    public Vector2 Acceleration = Vector2.Zero;
    public float MaxVelocity = 10.0f;
    public float BodySize = 20;
    public float Mass = 10;

    public void ApplyForce(Vector2 force) {
      Acceleration += force / Mass;
    }

    public void Move() {
      Velocity = (Velocity + Acceleration).Clamped(MaxVelocity);
      Position += Velocity;
      Acceleration = Vector2.Zero;

      BounceOnEdges();
    }

    public void BounceOnEdges() {
      var size = GetViewport().Size;

      if (Position.y < BodySize / 2 || Position.y > size.y - BodySize / 2) {
        Velocity.y *= -1;
      }

      if (Position.x < BodySize / 2 || Position.x > size.x - BodySize / 2) {
        Velocity.x *= -1;
      }
    }

    public override void _Ready() {
      var size = GetViewport().Size;
      var xPos = (float)GD.RandRange(BodySize, size.x - BodySize);
      Position = new Vector2(xPos, size.y / 2);
    }

    public override void _Process(float delta) {
      var wind = new Vector2(0.1f, 0);
      var gravity = new Vector2(0, 0.9f);

      ApplyForce(wind);
      ApplyForce(gravity);

      Move();
    }

    public override void _Draw() {
      DrawCircle(Vector2.Zero, BodySize, Colors.LightBlue.WithAlpha(200));
      DrawCircle(Vector2.Zero, BodySize - 2, Colors.White.WithAlpha(200));
    }
  }

  public override void _Ready() {
    foreach (var x in Enumerable.Range(0, 20)) {
      var mover = new Mover();
      mover.BodySize = (float)GD.RandRange(5, 20);
      mover.Mass = (float)GD.RandRange(5, 10);
      AddChild(mover);
    }
  }
}
