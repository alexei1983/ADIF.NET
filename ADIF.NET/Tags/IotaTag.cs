
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's IOTA designator.
  /// </summary>
  public class IOTATag : StringTag, ITag {

    /// <summary>
    /// Name of the tag.
    /// </summary>
    public override string Name => TagNames.IOTA;

    /// <summary>
    /// Creates a new IOTA tag.
    /// </summary>
    public IOTATag() { }

    /// <summary>
    /// Creates a new IOTA tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public IOTATag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsIOTADesignator();
      }
    }
  }
