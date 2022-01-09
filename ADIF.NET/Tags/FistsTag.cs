
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's FISTS CW Club member information.
  /// </summary>
  public class FISTSTag : StringTag, ITag {

    public override string Name => TagNames.Fists;

    public FISTSTag() { }

    public FISTSTag(string value) : base(value) { }
  }
}
