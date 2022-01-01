
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the callsign of the owner of the station used to log the contact.
  /// </summary>
  public class OwnerCallSignTag : StringTag, ITag {

    public override string Name => TagNames.OwnerCallSign;
    }
  }
