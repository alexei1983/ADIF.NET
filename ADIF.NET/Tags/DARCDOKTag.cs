
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's DARC DOK (District Location Code).
  /// </summary>
  public class DARCDOKTag : EnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.DARCDOK;

    /// <summary>
    /// 
    /// </summary>
    public override ADIFEnumeration Options => Values.DARCDOKs;

    /// <summary>
    /// Creates a new DARC_DOK tag.
    /// </summary>
    public DARCDOKTag() { }

    /// <summary>
    /// Creates a new DARC_DOK tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public DARCDOKTag(string value) : base(value) { }
  }
}
