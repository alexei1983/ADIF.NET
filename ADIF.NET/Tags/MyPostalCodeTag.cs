
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's postal code.
  /// </summary>
  public class MyPostalCodeTag : StringTag, ITag {

    public override string Name => TagNames.MyPostalCode;

    public MyPostalCodeTag() { }

    public MyPostalCodeTag(string value) : base(value) { }
  }
}
