using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's operator's name.
  /// </summary>
  [DisplayName("The contacted station's operator's name.")]
  public class NameTag : StringTag, ITag {

    public override string Name => TagNames.Name;
    }
  }
