
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's country code.
  /// </summary>
  public class DXCCTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.DXCC;

    public override ADIFEnumeration Options => Values.CountryCodes;

    public DXCCTag() { }

    public DXCCTag(string value) : base(value) { }
  }
}
