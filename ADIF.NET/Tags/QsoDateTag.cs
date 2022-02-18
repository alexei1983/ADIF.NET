using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSODateTag : DateTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.QSODate;

    /// <summary>
    /// 
    /// </summary>
    public QSODateTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public QSODateTag(DateTime value) : base(value) { }

  }
}
