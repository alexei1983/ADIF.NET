
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the mode of the QSO.
  /// </summary>
  public class ModeTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.Mode;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.Modes;

    /// <summary>
    /// Creates a new MODE tag.
    /// </summary>
    public ModeTag() { }

    /// <summary>
    /// Creates a new MODE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ModeTag(string value) : base(value) { }
  }
}
