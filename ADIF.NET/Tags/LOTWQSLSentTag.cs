﻿
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class LOTWQSLSentTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.LOTWQSLSentStatus;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.QSLSentStatuses;

    /// <summary>
    /// Creates a new LOTW_QSL_SENT tag.
    /// </summary>
    public LOTWQSLSentTag() { }

    /// <summary>
    /// Creates a new LOTW_QSL_SENT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public LOTWQSLSentTag(string value) : base(value) { }
  }
}
