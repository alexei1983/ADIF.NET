
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's IOTA designator.
  /// </summary>
  public class MyIotaTag : StringTag, ITag {

    public override string Name => TagNames.MyIOTA;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsIOTADesignator();
      }
    }
  }
