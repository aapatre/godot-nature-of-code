using Godot;

public class C5Example6 : Node2D, IExample
{
  public string _Summary()
  {
    return "Example 5.6:\n"
      + "Distance Joint\n\n"
      + "Using PinJoint2D to simulate distance joints.";
  }

  public class DoubleBall : Node2D
  {
    public float OutlineWidth = 2;
    public Color OutlineColor = Colors.LightBlue;
    public Color BaseColor = Colors.White;
    public float Distance = 10;
    public float Radius = 20;

    private SimpleBall ball1;
    private SimpleBall ball2;

    public override void _Ready()
    {
      ball1 = new SimpleBall();
      ball1.Radius = Radius;
      ball1.Position = new Vector2(-Distance, -Distance);
      ball2 = new SimpleBall();
      ball2.Radius = Radius;
      ball2.Position = new Vector2(Distance, Distance);
      AddChild(ball1);
      AddChild(ball2);

      var join = new PinJoint2D();
      join.NodeA = ball1.GetPath();
      join.NodeB = ball2.GetPath();
      join.Softness = 0.1f;
      ball1.AddChild(join);
    }

    public override void _Draw()
    {
      DrawLine(ball1.Position, ball2.Position, Colors.White, 2);
    }

    public override void _Process(float delta)
    {
      Update();
    }
  }

  private void SpawnBody(Vector2 position)
  {
    var body = new DoubleBall();
    body.Distance = 20;
    body.Radius = 10;
    body.RotationDegrees = Utils.RandRangef(0, 360);
    body.GlobalPosition = position;
    AddChild(body);
  }

  public override void _Ready()
  {
    var size = GetViewportRect().Size;
    var floorHeight = 25;
    var offset = 50;

    // Add left floor
    var leftFloor = new SimpleWall();
    leftFloor.BodySize = new Vector2(size.x / 2.5f, floorHeight);
    leftFloor.Position = new Vector2(size.x / 2.5f / 2 + offset, size.y);
    AddChild(leftFloor);

    // Add right floor
    var rightFloor = new SimpleWall();
    rightFloor.BodySize = new Vector2(size.x / 2.5f, floorHeight);
    rightFloor.Position = new Vector2(size.x - size.x / 2.5f / 2 - offset, size.y - offset * 2);
    AddChild(rightFloor);

    int bodyCount = 10;
    for (int i = 0; i < bodyCount; ++i)
    {
      SpawnBody(Utils.RandVector2(offset * 2, size.x - offset * 2, offset * 2, size.y - offset * 2));
    }
  }

  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event is InputEventScreenTouch eventScreenTouch)
    {
      if (eventScreenTouch.Pressed)
      {
        SpawnBody(eventScreenTouch.Position);
      }
    }

    if (@event is InputEventScreenDrag eventScreenDrag)
    {
      SpawnBody(eventScreenDrag.Position);
    }
  }
}