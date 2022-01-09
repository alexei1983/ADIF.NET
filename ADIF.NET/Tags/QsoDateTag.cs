using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSODateTag : DateTag, ITag {

    public override string Name => TagNames.QSODate;

    public QSODateTag() { }

    public QSODateTag(DateTime value) : base(value) { }

  }
}
