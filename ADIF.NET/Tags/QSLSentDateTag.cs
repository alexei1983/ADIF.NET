using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL sent date.
  /// </summary>
  public class QSLSentDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QSLSentDate;

    /// <summary>
    /// Creates a new QSLSDATE tag.
    /// </summary>
    public QSLSentDateTag() { }

    /// <summary>
    /// Creates a new QSLSDATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLSentDateTag(DateTime value) : base(value) { }
  }
}
