
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's 2-character, 4-character, 6-character, or 8-character Maidenhead Grid Square.
  /// </summary>
  public class MyGridSquareTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyGridSquare;

    /// <summary>
    /// Creates a new MY_GRIDSQUARE tag.
    /// </summary>
    public MyGridSquareTag() { }

    /// <summary>
    /// Creates a new MY_GRIDSQUARE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyGridSquareTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value)
    {
      if (value is null)
        return true;

      if (value is string strVal)
      {
        if (string.IsNullOrEmpty(strVal))
          return true;

        return strVal.IsADIFGridSquare();
      }
      return false;
    }
  }
}
