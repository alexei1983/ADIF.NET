using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  [DisplayName("")]
  public class SigInfoIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.SigInfoIntl;
    }
  }
