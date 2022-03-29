
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the callsign of the owner of the station used to log the contact.
  /// </summary>
  public class OwnerCallSignTag : BaseCallSignTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.OwnerCallSign;

    /// <summary>
    /// Creates a new OWNER_CALLSIGN tag.
    /// </summary>
    public OwnerCallSignTag() { }

    /// <summary>
    /// Creates a new OWNER_CALLSIGN tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public OwnerCallSignTag(string value) : base(value) { }
  }
}
