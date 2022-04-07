
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's IOTA designator.
  /// </summary>
  public class MyIOTATag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyIOTA;

    /// <summary>
    /// Creates a new MY_IOTA tag.
    /// </summary>
    public MyIOTATag() { }

    /// <summary>
    /// Creates a new MY_IOTA tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyIOTATag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsIOTADesignator();
      }
    }
  }
