
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's DARC DOK (District Location Code).
  /// </summary>
  public class DARCDOKTag : EnumerationTag, ITag {

    public override string Name => TagNames.DARCDOK;

    public DARCDOKTag() { }

    public DARCDOKTag(string value) : base(value) { }
  }
}
