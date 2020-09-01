using Godot;
using Drawing;
using Particles;

namespace Examples
{
  namespace Chapter4
  {
    /// <summary>
    /// Exercise 4.5 - Particle Systems Lifespan.
    /// </summary>
    /// Remove a particle system once the particle count is reached.
    public class C4Exercise5 : Node2D, IExample
    {
      public string _Summary()
      {
        return "Exercise 4.5:\n"
          + "Particle Systems Lifespan\n\n"
          + "Touch screen to spawn particle system";
      }

      private void AddParticleSystem(Vector2 position)
      {
        var ps = new SimpleParticleSystem();
        ps.ParticleCount = 200;
        ps.RemoveWhenEmptyParticles = true;
        ps.ParticleCreationFunction = () =>
        {
          var particle = new SimpleFallingParticle();
          particle.Lifespan = 2;
          particle.MeshSize = new Vector2(10, 10);
          particle.Mesh.MeshType = SimpleMesh.TypeEnum.Square;
          return particle;
        };
        ps.GlobalPosition = position;
        AddChild(ps);
      }

      public override void _UnhandledInput(InputEvent @event)
      {
        if (@event is InputEventScreenTouch eventScreenTouch)
        {
          if (eventScreenTouch.Pressed)
          {
            AddParticleSystem(eventScreenTouch.Position);
          }
        }
      }

      public override void _Ready()
      {
        // Initial systems
        var size = GetViewportRect().Size;
        int initialCount = 2;
        float splitSize = size.x / initialCount;

        for (int i = 0; i < initialCount; ++i)
        {
          AddParticleSystem(new Vector2(splitSize / 2 + splitSize * i, size.y / 4));
        }
      }
    }
  }
}
