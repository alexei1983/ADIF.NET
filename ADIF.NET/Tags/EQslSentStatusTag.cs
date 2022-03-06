
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the eQSL.cc QSL sent status.
  /// </summary>
  public class EQSLSentStatusTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.EQSLSentStatus;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.EQSLSentStatuses;

    /// <summary>
    /// Creates a new EQSL_QSL_SENT tag.
    /// </summary>
    public EQSLSentStatusTag() { }

    /// <summary>
    /// Creates a new EQSL_QSL_SENT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public EQSLSentStatusTag(string value) : base(value) { }
  }
}
