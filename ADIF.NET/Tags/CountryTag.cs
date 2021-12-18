
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's DXCC entity name.
  /// </summary>
  public class CountryTag : StringTag, ITag {

    public override string Name => TagNames.Country;
    }
  }
