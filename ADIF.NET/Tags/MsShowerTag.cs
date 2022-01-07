
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the meteor shower in a meteor scatter QSO.
  /// </summary>
  public class MsShowerTag : StringTag, ITag {

    public override string Name => TagNames.MsShower;

    public MsShowerTag() { }

    public MsShowerTag(string value) : base(value) { }
  }
}
