
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest QSO received serial number.
  /// </summary>
  public class SrxStringTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.SrxString;

    /// <summary>
    /// Creates a new SRX_STRING tag.
    /// </summary>
    public SrxStringTag() { }

    /// <summary>
    /// Creates a new SRX_STRING tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public SrxStringTag(string value) : base(value) { }
  }
}
