
namespace ADIF.NET.Tags {

  /// <summary>
  /// The contest check (e.g. for ARRL Sweepstakes).
  /// </summary>
  public class CheckTag : StringTag, ITag {

    public override string Name => TagNames.Check;
    }
  }
