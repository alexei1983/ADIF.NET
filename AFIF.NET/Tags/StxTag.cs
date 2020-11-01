using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest QSO transmitted serial number.
  /// </summary>
  [DisplayName("The contest QSO transmitted serial number.")]
  public class StxTag : NumberTag, ITag {

    public override string Name => TagNames.Stx;
    }
  }
