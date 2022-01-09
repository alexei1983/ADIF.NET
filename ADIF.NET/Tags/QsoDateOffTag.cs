using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSODateOffTag : DateTag, ITag {

    public override string Name => TagNames.QSODateOff;

    public QSODateOffTag() { }

    public QSODateOffTag(DateTime value) : base(value) { }

  }
}
