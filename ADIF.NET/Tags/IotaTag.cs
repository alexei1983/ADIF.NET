using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  [DisplayName("The contacted station's IOTA designator.")]
  public class IotaTag : StringTag, ITag {

    public override string Name => TagNames.Iota;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsIOTADesignator();
      }
    }
  }
