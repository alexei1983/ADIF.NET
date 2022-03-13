
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's complete mailing address: full name, 
  /// street address, city, postal code, and country.
  /// </summary>
  public class AddressIntlTag : IntlMultilineStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.AddressIntl;

    /// <summary>
    /// Creates a new ADDRESS_INTL tag.
    /// </summary>
    public AddressIntlTag() { }

    /// <summary>
    /// Creates a new ADDRESS_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public AddressIntlTag(string value) : base(value) { }
  }
}
