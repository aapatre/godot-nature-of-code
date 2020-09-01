using Godot;
using Drawing;

namespace Examples
{
  namespace Chapter0
  {
    /// <summary>
    /// Example 0.6 - 2D perlin noise.
    /// </summary>
    /// Generate a noise texture with SimpleNoiseTexture.
    public class C0Example6 : Node2D, IExample
    {
      public string _Summary()
      {
        return "Example I.6:\n"
          + "2D Perlin noise";
      }

      public override void _Ready()
      {
        var noiseTexture = new SimpleNoiseTexture();
        noiseTexture.Factor = 3;
        noiseTexture.Octaves = 8;
        AddChild(noiseTexture);
      }
    }
  }
}
