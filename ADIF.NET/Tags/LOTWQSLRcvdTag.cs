
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents whether the Log of the World (LOTW) QSL has been received.
  /// </summary>
  public class LOTWQSLRcvdTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.LOTWQSLRcvdStatus;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.QSLReceivedStatuses;

    /// <summary>
    /// Creates a new LOTW_QSL_RCVD tag.
    /// </summary>
    public LOTWQSLRcvdTag() { }

    /// <summary>
    /// Creates a new LOTW_QSL_RCVD tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public LOTWQSLRcvdTag(string value) : base(value) { }
  }
}
