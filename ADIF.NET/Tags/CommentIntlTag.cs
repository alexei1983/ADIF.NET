
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the comment field for the QSO.
  /// </summary>
  public class CommentIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.CommentIntl;

    public CommentIntlTag() { }

    public CommentIntlTag(string value) : base(value) { }
  }
}
