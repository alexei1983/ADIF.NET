using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSLSentDateTag : DateTag, ITag {

    public override string Name => TagNames.QSLSentDate;

    public QSLSentDateTag() { }

    public QSLSentDateTag(DateTime value) : base(value) { }

  }
}
