
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's QSL route.
  /// </summary>
  public class QSLViaTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QSLVia;

    /// <summary>
    /// Creates a new QSL_VIA tag.
    /// </summary>
    public QSLViaTag() { }

    /// <summary>
    /// Creates a new QSL_VIA tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLViaTag(string value) : base(value) { }
  }
}
