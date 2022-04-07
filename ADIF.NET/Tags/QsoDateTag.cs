using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date on which the QSO started.
  /// </summary>
  public class QSODateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QSODate;

    /// <summary>
    /// Creates a new QSO_DATE tag.
    /// </summary>
    public QSODateTag() { }

    /// <summary>
    /// Creates a new QSO_DATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSODateTag(DateTime value) : base(value) { }

  }
}
