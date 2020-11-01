using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's Secondary Administrative Subdivision.
  /// </summary>
  [DisplayName("The contacted station's Secondary Administrative Subdivision.")]
  public class CntyTag : StringTag, ITag {

    public override string Name => TagNames.Cnty;
    }
  }
