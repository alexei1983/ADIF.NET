using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class LOTWQSLReceivedDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.LOTWQSLReceivedDate;

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
