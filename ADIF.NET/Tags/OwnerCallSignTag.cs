using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the callsign of the owner of the station used to log the contact.
  /// </summary>
  [DisplayName("The callsign of the owner of the station used to log the contact (e.g. the callsign of the operator's host).")]
  public class OwnerCallSignTag : StringTag, ITag {

    public override string Name => TagNames.OwnerCallSign;
    }
  }
