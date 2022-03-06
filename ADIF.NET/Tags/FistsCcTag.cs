

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's FISTS CW Club Century Certificate (CC) number.
  /// </summary>
  public class FISTSCCTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.FistsCc;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 1;

    /// <summary>
    /// Creates a new FISTS_CC tag.
    /// </summary>
    public FISTSCCTag() { }

    /// <summary>
    /// Creates a new FISTS_CC tag.
    /// </summary>
    /// <param name="value"></param>
    public FISTSCCTag(double value) : base(value) { }
  }
}
