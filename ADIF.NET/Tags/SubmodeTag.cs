
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the mode of the QSO.
  /// </summary>
  public class SubmodeTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.Submode;

    public override ADIFEnumeration Options => Values.Modes;

    public SubmodeTag() { }

    public SubmodeTag(string value) : base(value) { }
  }
}
