
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's DXCC entity name.
  /// </summary>
  public class MyCountryIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyCountryIntl;

    public MyCountryIntlTag() { }

    public MyCountryIntlTag(string value) : base(value) { }
  }
}
