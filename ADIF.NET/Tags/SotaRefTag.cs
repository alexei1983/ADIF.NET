
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class SOTARefTag : StringTag, ITag {

    public override string Name => TagNames.SOTARef;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsSOTADesignator();
      }
    }
  }
