
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the satellite mode.
  /// </summary>
  public class SatModeTag : StringTag, ITag {

    public override string Name => TagNames.SatMode;

    public SatModeTag() { }

    public SatModeTag(string value) : base(value) { }
  }
}
