
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's IOTA designator.
  /// </summary>
  public class IotaTag : StringTag, ITag {

    public override string Name => TagNames.IOTA;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsIOTADesignator();
      }
    }
  }
