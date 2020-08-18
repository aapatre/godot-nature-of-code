using Godot;

public static class ColorExtensions
{
  public static Color WithAlpha(this Color color, byte alpha)
  {
    Color clone = color;
    clone.a8 = alpha;
    return clone;
  }
}

public static class Utils
{
  /**
   * Map a value from one bound to another.
   */
  static public float Map(float value, float istart, float istop, float ostart, float ostop)
  {
    return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
  }

  /**
   * Return a signed Randf, between -1 and 1.
   */
  static public float SignedRandf()
  {
    return Map((float)GD.RandRange(0, 1), 0, 1, -1, 1);
  }

  static public float RandRangef(float min, float max)
  {
    return (float)GD.RandRange(min, max);
  }

  static public Vector2 RandRangeVector2X(float minX, float maxX, float y)
  {
    return new Vector2(RandRangef(minX, maxX), y);
  }

  static public Vector2 RandRangeVector2Y(float x, float minY, float maxY)
  {
    return new Vector2(x, RandRangef(minY, maxY));
  }

  /**
   * Return a random Vector2.
   */
  static public Vector2 RandVector2(Vector2 rangeX, Vector2 rangeY)
  {
    return RandVector2(rangeX.x, rangeX.y, rangeY.x, rangeY.y);
  }

  /**
   * Return a random Vector2.
   */
  static public Vector2 RandVector2(float minX, float maxX, float minY, float maxY)
  {
    var vec = Vector2.Zero;
    vec.x = (float)GD.RandRange(minX, maxX);
    vec.y = (float)GD.RandRange(minY, maxY);

    return vec;
  }

  static public Color RandColor()
  {
    return new Color((float)GD.RandRange(0, 1), (float)GD.RandRange(0, 1), (float)GD.RandRange(0, 1));
  }

  static private Font DEFAULT_FONT = null;

  static public Font LoadDefaultFont()
  {
    if (DEFAULT_FONT == null)
    {
      var fontData = GD.Load("res://assets/fonts/Raleway-Regular.ttf");
      var dynamicFont = new DynamicFont();
      dynamicFont.FontData = (DynamicFontData)fontData;
      dynamicFont.Size = 16;
      dynamicFont.UseFilter = true;
      dynamicFont.OutlineSize = 1;
      dynamicFont.OutlineColor = Colors.Black;
      DEFAULT_FONT = dynamicFont;
    }

    return DEFAULT_FONT;
  }
}
