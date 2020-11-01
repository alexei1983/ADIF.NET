using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's antenna.
  /// </summary>
  [DisplayName("The logging station's antenna.")]
  public class MyAntennaIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyAntennaIntl;
    }
  }
