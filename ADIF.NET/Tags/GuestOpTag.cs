using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging operator's callsign.
  /// </summary>
  [DeprecatedTag(ADIFTags.Operator)]
  public class GuestOpTag : BaseCallSignTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.GuestOp;

    /// <summary>
    /// Creates a new GUEST_OP tag.
    /// </summary>
    public GuestOpTag() { }

    /// <summary>
    /// Creates a new GUEST_OP tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public GuestOpTag(string value) : base(value) { }
  }
}
