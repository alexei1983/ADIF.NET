using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSODateOffTag : DateTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.QSODateOff;

    /// <summary>
    /// 
    /// </summary>
    public QSODateOffTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public QSODateOffTag(DateTime value) : base(value) { }
  }
}
