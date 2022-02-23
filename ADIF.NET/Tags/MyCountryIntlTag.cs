
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's DXCC entity name.
  /// </summary>
  public class MyCountryIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyCountryIntl;

    /// <summary>
    /// Creates a new MY_COUNTRY_INTL tag.
    /// </summary>
    public MyCountryIntlTag() { }

    /// <summary>
    /// Creates a new MY_COUNTRY_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyCountryIntlTag(string value) : base(value) { }
  }
}
