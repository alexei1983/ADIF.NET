using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the logging station's special activity or interest group.
  /// </summary>
  [DisplayName("The name of the logging station's special activity or interest group.")]
  public class MySigTag : StringTag, ITag {

    public override string Name => TagNames.MySig;
    }
  }
