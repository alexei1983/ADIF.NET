using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's Secondary Administrative Subdivision.
  /// </summary>
  [DisplayName("The logging station's Secondary Administrative Subdivision.")]
  public class MyCntyTag : StringTag, ITag {

    public override string Name => TagNames.MyCnty;
    }
  }
