
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's country code.
  /// </summary>
  public class MyDXCCTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.MyDXCC;

    public MyDXCCTag() { }

    public MyDXCCTag(string value) : base(value) { }

    public override ADIFEnumeration Options => Values.CountryCodes;
    }
  }
