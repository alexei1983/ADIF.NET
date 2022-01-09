
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's complete mailing address: full name, 
  /// street address, city, postal code, and country.
  /// </summary>
  public class AddressIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.AddressIntl;

    public AddressIntlTag() { }

    public AddressIntlTag(string value) : base(value) { }
  }
}
