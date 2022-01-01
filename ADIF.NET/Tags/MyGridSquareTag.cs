using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's 2-character, 4-character, 6-character, or 8-character Maidenhead Grid Square.
  /// </summary>
  public class MyGridSquareTag : StringTag, ITag {

    public override string Name => TagNames.MyGridSquare;
    }
  }
