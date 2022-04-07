using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date the QSL was received from ARRL Logbook of the World.
  /// </summary>
  public class LOTWQSLReceivedDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.LOTWQSLReceivedDate;

    /// <summary>
    /// Creates a new LOTW_QSLRDATE tag.
    /// </summary>
    public LOTWQSLReceivedDateTag() { }

    /// <summary>
    /// Creates a new LOTW_QSLRDATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public LOTWQSLReceivedDateTag(DateTime value) : base(value) { }

  }
}
