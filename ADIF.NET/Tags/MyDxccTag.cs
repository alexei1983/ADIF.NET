
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's country code.
  /// </summary>
  public class MyDXCCTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyDXCC;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.CountryCodes;

    /// <summary>
    /// Creates a new MY_DXCC tag.
    /// </summary>
    public MyDXCCTag() { }

    /// <summary>
    /// Creates a new MY_DXCC tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyDXCCTag(string value) : base(value) { }
  }
}
