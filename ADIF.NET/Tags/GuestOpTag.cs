using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging operator's callsign.
  /// </summary>
  [DeprecatedTag(TagNames.Operator)]
  public class GuestOpTag : StringTag, ITag {

    public override string Name => TagNames.GuestOp;
    }
  }
