
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the list of awards submitted to a sponsor.
  /// </summary>
  public class AwardSubmittedTag : SponsoredAwardListTag, ITag {

    public override string Name => TagNames.AwardSubmitted;

    public AwardSubmittedTag() { }

    public AwardSubmittedTag(string value) : base(value) { }
  }
}
