using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the comment field for the QSO.
  /// </summary>
  [DisplayName("The comment field for the QSO.")]
  public class CommentTag : StringTag, ITag {

    public override string Name => TagNames.Comment;
    }
  }
