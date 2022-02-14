
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the list of awards granted by a sponsor.
  /// </summary>
  public class AwardGrantedTag : SponsoredAwardListTag, ITag {

    public override string Name => TagNames.AwardGranted;

    public AwardGrantedTag() { }

    public AwardGrantedTag(string value) : base(value) { }
  }
}
