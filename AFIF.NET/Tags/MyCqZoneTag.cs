using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's CQ Zone.
  /// </summary>
  [DisplayName("The logging station's CQ Zone.")]
  public class MyCqZoneTag : NumberTag, ITag {

    public override string Name => TagNames.MyCqZone;
    }
  }
