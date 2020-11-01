using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's city.
  /// </summary>
  [DisplayName("The logging station's city.")]
  public class MyCityTag : StringTag, ITag {

    public override string Name => TagNames.MyCity;
    }
  }
