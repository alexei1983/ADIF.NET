using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  [DisplayName("")]
  public class TimeOnTag : TimeTag, ITag {

    public override string Name => TagNames.TimeOn;
    }
  }
