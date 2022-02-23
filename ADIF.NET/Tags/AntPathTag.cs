
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the signal path.
  /// </summary>
  public class AntPathTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.AntPath;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.AntennaPaths;

    /// <summary>
    /// Creates a new ANT_PATH tag.
    /// </summary>
    public AntPathTag() { }

    /// <summary>
    /// Creates a new ANT_PATH tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public AntPathTag(string value) : base(value) { }
  }
}
