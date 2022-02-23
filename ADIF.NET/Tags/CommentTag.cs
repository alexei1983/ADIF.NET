
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the comment field for the QSO.
  /// </summary>
  public class CommentTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Comment;

    /// <summary>
    /// Creates a new COMMENT tag.
    /// </summary>
    public CommentTag() { }

    /// <summary>
    /// Creates a new COMMENT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public CommentTag(string value) : base(value) { }
  }
}
