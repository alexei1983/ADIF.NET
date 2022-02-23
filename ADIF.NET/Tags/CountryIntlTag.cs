
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's DXCC entity name.
  /// </summary>
  public class CountryIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.CountryIntl;

    /// <summary>
    /// Creates a new COUNTRY_INTL tag.
    /// </summary>
    public CountryIntlTag() { }

    /// <summary>
    /// Creates a new COUNTRY_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public CountryIntlTag(string value) : base(value) { }
  }
}
