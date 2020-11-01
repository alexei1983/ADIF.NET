using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging operator's callsign.
  /// </summary>
  [DisplayName("The logging operator's callsign. (Deprecated: use OPERATOR instead)")]
  [DeprecatedTag]
  public class GuestOpTag : StringTag, ITag {

    public override string Name => TagNames.GuestOp;
    }
  }
