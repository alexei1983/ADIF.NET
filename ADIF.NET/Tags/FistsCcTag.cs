

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's FISTS CW Club Century Certificate (CC) number.
  /// </summary>
  public class FISTSCCTag : StringTag, ITag {

    public override string Name => TagNames.FistsCc;

    public FISTSCCTag() { }

    public FISTSCCTag(string value) : base(value) { }
  }
}
