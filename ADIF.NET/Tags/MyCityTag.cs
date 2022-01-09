
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's city.
  /// </summary>
  public class MyCityTag : StringTag, ITag {

    public override string Name => TagNames.MyCity;

    public MyCityTag() { }

    public MyCityTag(string value) : base(value) { }
  }
}
