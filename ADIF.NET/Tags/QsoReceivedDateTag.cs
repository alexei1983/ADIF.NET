using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSOReceivedDateTag : DateTag, ITag {

    public override string Name => TagNames.QSOReceivedDate;

    public QSOReceivedDateTag() { }

    public QSOReceivedDateTag(DateTime value) : base(value) { }

  }
}
