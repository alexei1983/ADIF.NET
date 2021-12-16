
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's callsign.
  /// </summary>
  public class CallTag : StringTag, ITag {

    public override string Name => TagNames.Call;
    }
  }
