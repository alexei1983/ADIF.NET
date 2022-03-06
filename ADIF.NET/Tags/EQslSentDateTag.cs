using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date the QSL was sent to eQSL.cc.
  /// </summary>
  public class EQSLSentDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.EQSLSentDate;

    /// <summary>
    /// Creates a new EQSL_QSLSDATE tag.
    /// </summary>
    public EQSLSentDateTag() { }

    /// <summary>
    /// Creates a new EQSL_QSLSDATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public EQSLSentDateTag(DateTime value) : base(value) { }
  }
}
