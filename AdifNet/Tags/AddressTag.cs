
namespace org.goodspace.Data.Radio.Adif.Tags {

  /// <summary>
  /// Represents the contacted station's complete mailing address: full name, 
  /// street address, city, postal code, and country.
  /// </summary>
  public class AddressTag : MultilineStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => AdifTags.Address;

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
