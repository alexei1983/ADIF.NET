using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL received date.
  /// </summary>
  public class QSLRvcdDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QSLRcvdDate;

    /// <summary>
    /// Creates a new QSLRDATE tag.
    /// </summary>
    public QSLRvcdDateTag() { }

    /// <summary>
    /// Creates a new QSLRDATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLRvcdDateTag(DateTime value) : base(value) { }

  }
}
