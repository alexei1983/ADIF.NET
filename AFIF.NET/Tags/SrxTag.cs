using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest QSO received serial number.
  /// </summary>
  [DisplayName("The contest QSO received serial number.")]
  public class SrxTag : NumberTag, ITag {

    public override string Name => TagNames.Srx;
    }
  }
