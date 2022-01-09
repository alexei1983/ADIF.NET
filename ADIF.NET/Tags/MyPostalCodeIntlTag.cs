
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's postal code.
  /// </summary>
  public class MyPostalCodeIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyPostalCodeIntl;

    public MyPostalCodeIntlTag() { }

    public MyPostalCodeIntlTag(string value) : base(value) { }
  }
}
