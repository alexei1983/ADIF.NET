
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the code for the logging station's Primary Administrative Subdivision (e.g. US State, JA Island, VE Province).
  /// </summary>
  public class StateTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.State;

    public override ADIFEnumeration Options => Values.CountryCodes;

    public StateTag() { }

    public StateTag(string value) : base(value) { }
  }
}
