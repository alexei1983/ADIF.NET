
namespace ADIF.NET.Tags {
  public class SOTARefTag : StringTag, ITag {

    public override string Name => TagNames.SotaRef;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsSOTADesignator();
      }
    }
  }
