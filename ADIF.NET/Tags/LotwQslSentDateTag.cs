﻿using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class LOTWQSLSentDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.LOTWQSLSentDate;

    /// <summary>
    /// Creates a new LOTW_QSLSDATE tag.
    /// </summary>
    public LOTWQSLSentDateTag() { }

    /// <summary>
    /// Creates a new LOTW_QSLSDATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public LOTWQSLSentDateTag(DateTime value) : base(value) { }

  }
}
