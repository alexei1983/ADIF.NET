using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  [DisplayName("The logging station's IOTA designator.")]
  public class MyIotaTag : StringTag, ITag {

    public override string Name => TagNames.MyIota;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsIotaDesignator();
      }
    }
  }
