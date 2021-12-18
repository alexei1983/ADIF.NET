
namespace ADIF.NET.Tags {
  public class MySotaRefTag : StringTag, ITag {

    public override string Name => TagNames.MySOTARef;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsSOTADesignator();
      }
    }
  }
