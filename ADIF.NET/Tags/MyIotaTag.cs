
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's IOTA designator.
  /// </summary>
  public class MyIOTATag : StringTag, ITag {

    public override string Name => TagNames.MyIOTA;

    public MyIOTATag() { }

    public MyIOTATag(string value) : base(value) { }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsIOTADesignator();
      }
    }
  }
