
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest QSO transmitted serial number.
  /// </summary>
  public class StxTag : NumberTag, ITag {

    public override string Name => TagNames.Stx;
    }
  }
