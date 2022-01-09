
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's 2-character, 4-character, 6-character, or 8-character Maidenhead Grid Square.
  /// </summary>
  public class GridSquareTag : StringTag, ITag {

    public override string Name => TagNames.GridSquare;

    public GridSquareTag() { }

    public GridSquareTag(string value) : base(value) { }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.IsADIFGridSquare();
      }
    }
  }
