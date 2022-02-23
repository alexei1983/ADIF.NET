
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's country code.
  /// </summary>
  public class DXCCTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.DXCC;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.CountryCodes;

    /// <summary>
    /// Creates a new DXCC tag.
    /// </summary>
    public DXCCTag() { }

    /// <summary>
    /// Creates a new DXCC tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public DXCCTag(string value) : base(value) { }
  }
}
