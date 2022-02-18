using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSLSentDateTag : DateTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.QSLSentDate;

    /// <summary>
    /// 
    /// </summary>
    public QSLSentDateTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public QSLSentDateTag(DateTime value) : base(value) { }

  }
}
