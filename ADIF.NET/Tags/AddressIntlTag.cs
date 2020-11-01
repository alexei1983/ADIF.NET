using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's complete mailing address: full name, 
  /// street address, city, postal code, and country.
  /// </summary>
  [DisplayName("The contacted station's complete mailing address: full name, street address, city, postal code, and country.")]
  public class AddressIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.AddressIntl;
    }
  }
