
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's city.
  /// </summary>
  public class QTHTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QTH;

    /// <summary>
    /// Creates a new QTH tag.
    /// </summary>
    public QTHTag() { }

    /// <summary>
    /// Creates a new QTH tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QTHTag(string value) : base(value) { }
  }
}
