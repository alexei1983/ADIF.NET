
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the eQSL.cc QSL received status.
  /// </summary>
  public class EQSLReceivedStatusTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.EQSLReceivedStatus;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.EQSLReceivedStatuses;

    /// <summary>
    /// Creates a new EQSL_QSL_RCVD tag.
    /// </summary>
    public EQSLReceivedStatusTag() { }

    /// <summary>
    /// Creates a new EQSL_QSL_RCVD tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public EQSLReceivedStatusTag(string value) : base(value) { }
  }
}
