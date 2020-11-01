using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's DXCC entity name.
  /// </summary>
  [DisplayName("The logging station's DXCC entity name.")]
  public class MyCountryTag : StringTag, ITag {

    public override string Name => TagNames.MyCountry;
    }
  }
