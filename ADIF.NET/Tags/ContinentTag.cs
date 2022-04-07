
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the continent of the contacted station.
  /// </summary>
  public class ContinentTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.Continent;

    /// <summary>
    /// Valid enumeration options.
    /// </summary>
    public override ADIFEnumeration Options => Values.Continents;

    /// <summary>
    /// Creates a new CONT tag.
    /// </summary>
    public ContinentTag() { }

    /// <summary>
    /// Creates a new CONT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ContinentTag(string value) : base(value) { }
  }
}
