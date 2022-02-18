using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSLRvcdDateTag : DateTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.QSLRcvdDate;

    /// <summary>
    /// 
    /// </summary>
    public QSLRvcdDateTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public QSLRvcdDateTag(DateTime value) : base(value) { }

  }
}
