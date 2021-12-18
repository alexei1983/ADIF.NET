
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the comment field for the QSO.
  /// </summary>
  public class CommentTag : StringTag, ITag {

    public override string Name => TagNames.Comment;
    }
  }
