

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's FISTS CW Club Century Certificate (CC) number.
  /// </summary>
  public class FISTSCCTag : PositiveIntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.FistsCc;

    /// <summary>
    /// Creates a new FISTS_CC tag.
    /// </summary>
    public FISTSCCTag() { }

    /// <summary>
    /// Creates a new FISTS_CC tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public FISTSCCTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new FISTS_CC tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public FISTSCCTag(int value) : base(value) { }
  }
}
