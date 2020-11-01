using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's owner's callsign.
  /// </summary>
  [DisplayName("The contacted station's owner's callsign.")]
  public class EqCallTag : StringTag, ITag {

    public override string Name => TagNames.EqCall;
    }
  }
