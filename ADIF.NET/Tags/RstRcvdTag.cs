
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the signal report from the contacted station.
  /// </summary>
  public class RstRcvdTag : StringTag, ITag {

    public override string Name => TagNames.RstRcvd;

    public RstRcvdTag() { }

    public RstRcvdTag(string value) : base(value) { }
  }
}
