
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the signal report sent to the contacted station.
  /// </summary>
  public class RstSentTag : StringTag, ITag {

    public override string Name => TagNames.RstSent;

    public RstSentTag() { }

    public RstSentTag(string value) : base(value) { }
  }
}
