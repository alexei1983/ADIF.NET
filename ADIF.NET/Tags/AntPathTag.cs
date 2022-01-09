
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the signal path.
  /// </summary>
  public class AntPathTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.AntPath;

    public override ADIFEnumeration Options => Values.AntennaPaths;

    public AntPathTag() { }

    public AntPathTag(string value) : base(value) { }
  }
}
