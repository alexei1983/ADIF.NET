using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's postal code.
  /// </summary>
  [DisplayName("The logging station's postal code.")]
  public class MyPostalCodeIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyPostalCodeIntl;
    }
  }
