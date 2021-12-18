using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's 2-character, 4-character, 6-character, or 8-character Maidenhead Grid Square.
  /// </summary>
  [DisplayName("The contacted station's 2-character, 4-character, 6-character, or 8-character Maidenhead Grid Square.")]
  public class GridSquareTag : StringTag, ITag {

    public override string Name => TagNames.GridSquare;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.IsADIFGridSquare();
      }
    }
  }
