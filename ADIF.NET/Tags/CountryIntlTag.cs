
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's DXCC entity name.
  /// </summary>
  public class CountryIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.CountryIntl;

    public CountryIntlTag() { }

    public CountryIntlTag(string value) : base(value) { }
  }
}
