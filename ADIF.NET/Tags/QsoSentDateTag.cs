using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSOSentDateTag : DateTag, ITag {

    public override string Name => TagNames.QSOSentDate;

    public QSOSentDateTag() { }

    public QSOSentDateTag(DateTime value) : base(value) { }

  }
}
