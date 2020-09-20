using Godot;
using Automata;

namespace Examples.Chapter7
{
  /// <summary>
  /// Exercise 7.7: Wrapping Game of Life.
  /// </summary>
  public class C7Exercise7 : Node2D, IExample
  {
    public string GetSummary()
    {
      return "Exercise 7.7:\nWrapping Game of Life";
    }

    public override void _Ready()
    {
      var ca = new CellularAutomata2D();
      AddChild(ca);

      ca.TouchBehavior = TouchBehaviorEnum.DrawCell;
      ca.WrapBehavior = WrapBehaviorEnum.Wrap;
      ca.RandomizeGrid();
    }
  }
}