using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's DXCC entity name.
  /// </summary>
  [DisplayName("The contacted station's DXCC entity name.")]
  public class CountryTag : StringTag, ITag {

    public override string Name => TagNames.Country;
    }
  }
