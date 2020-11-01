using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's DXCC entity name.
  /// </summary>
  [DisplayName("The contacted station's DXCC entity name.")]
  public class CountryIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.CountryIntl;
    }
  }
