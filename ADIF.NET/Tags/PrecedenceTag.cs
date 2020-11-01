using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest precedence (e.g. for ARRL sweepstakes).
  /// </summary>
  [DisplayName("The contest precedence (e.g. for ARRL sweepstakes).")]
  public class PrecedenceTag : StringTag, ITag {

    public override string Name => TagNames.Precedence;
    }
  }
