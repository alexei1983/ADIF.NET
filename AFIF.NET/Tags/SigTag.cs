using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the contacted station's special activity or interest group.
  /// </summary>
  [DisplayName("The name of the contacted station's special activity or interest group.")]
  public class SigTag : StringTag, ITag {

    public override string Name => TagNames.Sig;
    }
  }
