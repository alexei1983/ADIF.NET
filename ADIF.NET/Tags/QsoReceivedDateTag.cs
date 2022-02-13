using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSLRvcdDateTag : DateTag, ITag {

    public override string Name => TagNames.QSLRcvdDate;

    public QSLRvcdDateTag() { }

    public QSLRvcdDateTag(DateTime value) : base(value) { }

  }
}
