
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the mode of the QSO.
  /// </summary>
  public class ModeTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.Mode;

    public override ADIFEnumeration Options => Values.Modes;

    public ModeTag() { }

    public ModeTag(string value) : base(value) { }
  }
}
