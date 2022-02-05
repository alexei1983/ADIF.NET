
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the code for the logging station's Primary Administrative Subdivision (e.g. US State, JA Island, VE Province).
  /// </summary>
  public class MyStateTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.MyState;

    //public override ADIFEnumeration Options => Values.CountryCodes;

    public MyStateTag() { }

    public MyStateTag(string value) : base(value) { }
  }
}
