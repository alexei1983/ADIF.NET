using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  [DisplayName("")]
  public class TimeOffTag : TimeTag, ITag {

    public override string Name => TagNames.TimeOff;
    }
  }
