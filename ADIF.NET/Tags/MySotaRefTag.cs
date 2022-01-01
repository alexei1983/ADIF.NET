
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class MySotaRefTag : StringTag, ITag {

    public override string Name => TagNames.MySOTARef;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsSOTADesignator();
      }
    }
  }
