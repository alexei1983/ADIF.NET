
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's complete mailing address: full name, 
  /// street address, city, postal code, and country.
  /// </summary>
  public class AddressTag : StringTag, ITag {

    public override string Name => TagNames.Address;

    /// <summary>
    /// Creates a new ADDRESS tag.
    /// </summary>
    public AddressTag() { }

    /// <summary>
    /// Creates a new ADDRESS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public AddressTag(string value) : base(value) { }
  }
}
