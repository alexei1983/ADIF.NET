using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's city.
  /// </summary>
  [DisplayName("The logging station's city.")]
  public class MyCityIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyCityIntl;
    }
  }
