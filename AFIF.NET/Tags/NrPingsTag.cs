using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the number of meteor scatter pings heard by the logging station.
  /// </summary>
  [DisplayName("The number of meteor scatter pings heard by the logging station.")]
  public class NrPingsTag : NumberTag, ITag {

    public override string Name => TagNames.NrPings;
    }
  }
