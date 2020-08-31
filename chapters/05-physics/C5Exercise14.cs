using Godot;
using VerletPhysics;
using System.Collections.Generic;

public class C5Exercise14 : Node2D, IExample
{
  public string _Summary()
  {
    return "Exercise 5.14:\n"
      + "Verlet Creature\n\n"
      + "You can move points by touching them\n"
      + "If you drag points quick enough, links will break";
  }

  private class VerletCreature
  {
    public VerletCreature(VerletWorld world, Vector2 centerPosition, float height, float gravityScale = 1, float pointRadius = 10f, bool drawPoints = true, bool drawSupportLinks = false)
    {
      float tearSensitivityFactor = 4;
      float stiffness = 0.10f;
      Color supportLinkColor = Colors.LightCyan.WithAlpha(64);
      float sep = height / 4;

      VerletPoint createPoint()
      {
        return world.CreatePoint(centerPosition + MathUtils.RandVector2(-5, 5, -5, 5), radius: pointRadius, visible: drawPoints);
      }

      VerletLink createLink(VerletPoint a, VerletPoint b, float distance)
      {
        return world.CreateLink(a, b, restingDistance: distance, tearSensitivityFactor: tearSensitivityFactor, stiffness: stiffness);
      }

      VerletLink createSupportLink(VerletPoint a, VerletPoint b, float distance)
      {
        return world.CreateLink(a, b, restingDistance: distance, tearSensitivityFactor: -1, stiffness: stiffness, color: supportLinkColor, visible: drawSupportLinks);
      }

      // Head
      var topLeft = createPoint();
      var topMiddle = createPoint();
      var topRight = createPoint();
      var topLeftSide = createPoint();
      var topRightSide = createPoint();
      var neckLeft = createPoint();
      var neckRight = createPoint();
      createLink(topMiddle, topRight, sep);
      createLink(topRight, topRightSide, sep);
      createLink(topRightSide, neckRight, sep);
      createLink(topMiddle, topLeft, sep);
      createLink(topLeft, topLeftSide, sep);
      createLink(topLeftSide, neckLeft, sep);
      createSupportLink(neckLeft, neckRight, sep * 2);

      // Body
      var bodyLeftSide = createPoint();
      var bodyRightSide = createPoint();
      var bottomLeft = createPoint();
      var bottomMidLeft = createPoint();
      var bottomMiddle = createPoint();
      var bottomMidRight = createPoint();
      var bottomRight = createPoint();
      createLink(bodyRightSide, bottomRight, sep);
      createLink(bottomRight, bottomMidRight, sep);
      createLink(bottomMidRight, bottomMiddle, sep);
      createLink(bodyLeftSide, bottomLeft, sep);
      createLink(bottomLeft, bottomMidLeft, sep);
      createLink(bottomMidLeft, bottomMiddle, sep);
      createSupportLink(bodyLeftSide, bodyRightSide, sep * 3);

      // Attach
      createLink(neckLeft, bodyLeftSide, sep);
      createLink(neckRight, bodyRightSide, sep);
      createSupportLink(topMiddle, bottomMiddle, height);
      createSupportLink(topLeftSide, bottomMidRight, height * 1.1f);
      createSupportLink(topRightSide, bottomMidLeft, height * 1.1f);
      createSupportLink(topLeft, bottomRight, height * 1.1f);
      createSupportLink(topRight, bottomLeft, height * 1.1f);
    }
  }

  public override void _Ready()
  {
    var size = GetViewportRect().Size;
    var physics = new VerletWorld();
    physics.AddBehavior(new GravityBehavior());
    AddChild(physics);

    var creature = new VerletCreature(
      physics,
      centerPosition: size / 2,
      height: 200,
      gravityScale: 0.5f,
      drawSupportLinks: true
    );

    var ragdoll = new VerletRagdoll(
      physics,
      new Vector2(size.x * 0.75f, size.y / 2),
      height: 200,
      pointRadius: 5f,
      gravityScale: 0.25f,
      tearSensitivityFactor: 4,
      drawIntermediatePoints: false,
      drawSupportLinks: true
    );
  }
}
