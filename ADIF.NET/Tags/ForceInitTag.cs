
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the new EME "initial".
  /// </summary>
  public class ForceInitTag : BooleanTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.ForceInit;

    /// <summary>
    /// Creates a new FORCE_INIT tag.
    /// </summary>
    public ForceInitTag() { }

    /// <summary>
    /// Creates a new FORCE_INIT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ForceInitTag(bool value) : base(value) { }
  }
}
