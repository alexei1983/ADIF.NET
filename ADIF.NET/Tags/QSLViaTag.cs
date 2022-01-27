
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's QSL route.
  /// </summary>
  public class QSLViaTag : StringTag, ITag {

    public override string Name => TagNames.QSLVia;

    public QSLViaTag() { }

    public QSLViaTag(string value) : base(value) { }
  }
}
