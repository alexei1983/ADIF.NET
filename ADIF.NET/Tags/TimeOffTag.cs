using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class TimeOffTag : TimeTag, ITag {

    public override string Name => TagNames.TimeOff;

    public TimeOffTag() { }

    public TimeOffTag(DateTime value) : base(value) { }
  }
}
