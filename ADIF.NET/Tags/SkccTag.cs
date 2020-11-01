using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's Straight Key Century Club (SKCC) member information.
  /// </summary>
  [DisplayName("The contacted station's Straight Key Century Club (SKCC) member information.")]
  public class SkccTag : StringTag, ITag {

    public override string Name => TagNames.Skcc;
    }
  }
