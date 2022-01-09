using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class TimeOnTag : TimeTag, ITag {

    public override string Name => TagNames.TimeOn;

    public TimeOnTag() { }

    public TimeOnTag(DateTime value) : base(value) { }
  }
}
