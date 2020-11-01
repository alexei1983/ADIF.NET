using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's callsign.
  /// </summary>
  [DisplayName("The contacted station's callsign.")]
  public class CallTag : StringTag, ITag {

    public override string Name => TagNames.Call;
    }
  }
