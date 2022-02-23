
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's Secondary Administrative Subdivision.
  /// </summary>
  public class CntyTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Cnty;

    /// <summary>
    /// Creates a new CNTY tag.
    /// </summary>
    public CntyTag() { }

    /// <summary>
    /// Creates a new CNTY tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public CntyTag(string value) : base(value) { }
  }
}
