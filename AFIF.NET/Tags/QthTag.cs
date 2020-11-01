using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's city.
  /// </summary>
  [DisplayName("The contacted station's city.")]
  public class QthTag : StringTag, ITag {

    public override string Name => TagNames.Qth;
    }
  }
