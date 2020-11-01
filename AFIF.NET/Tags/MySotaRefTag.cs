
namespace ADIF.NET.Tags {
  public class MySotaRefTag : StringTag, ITag {

    public override string Name => TagNames.MySotaRef;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsSotaDesignator();
      }
    }
  }
