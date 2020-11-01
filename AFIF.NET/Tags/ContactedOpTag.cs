using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the callsign of the individual operating the contacted station.
  /// </summary>
  [DisplayName("The callsign of the individual operating the contacted station.")]
  public class ContactedOpTag : StringTag, ITag {

    public override string Name => TagNames.ContactedOp;
    }
  }
