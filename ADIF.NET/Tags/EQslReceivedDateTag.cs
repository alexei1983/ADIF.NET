using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date the QSL was received from eQSL.cc.
  /// </summary>
  public class EQSLReceivedDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.EQSLReceivedDate;

    /// <summary>
    /// Creates a new EQSL_QSLRDATE tag.
    /// </summary>
    public EQSLReceivedDateTag() { }

    /// <summary>
    /// Creates a new EQSL_QSLRDATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public EQSLReceivedDateTag(DateTime value) : base(value) { }
  }
}
