
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest precedence (e.g. for ARRL sweepstakes).
  /// </summary>
  public class PrecedenceTag : StringTag, ITag {

    public override string Name => TagNames.Precedence;
    }
  }
