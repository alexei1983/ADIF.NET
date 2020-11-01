using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging operator's name.
  /// </summary>
  [DisplayName("The logging operator's name.")]
  public class MyNameIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyNameIntl;
    }
  }
