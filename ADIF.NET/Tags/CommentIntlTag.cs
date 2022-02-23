
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the comment field for the QSO.
  /// </summary>
  public class CommentIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.CommentIntl;

    /// <summary>
    /// Creates a new COMMENT_INTL tag.
    /// </summary>
    public CommentIntlTag() { }

    /// <summary>
    /// Creates a new COMMENT_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public CommentIntlTag(string value) : base(value) { }
  }
}
