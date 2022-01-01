
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's DXCC entity name.
  /// </summary>
  public class MyCountryTag : StringTag, ITag {

    public override string Name => TagNames.MyCountry;
    }
  }
