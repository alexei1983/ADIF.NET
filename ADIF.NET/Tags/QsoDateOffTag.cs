using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date on which the QSO ended.
  /// </summary>
  public class QSODateOffTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QSODateOff;

    /// <summary>
    /// Creates a new QSO_DATE_OFF tag.
    /// </summary>
    public QSODateOffTag() { }

    /// <summary>
    /// Creates a new QSO_DATE_OFF tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSODateOffTag(DateTime value) : base(value) { }
  }
}
