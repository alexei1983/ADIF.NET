using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the comment field for the QSO.
  /// </summary>
  [DisplayName("The comment field for the QSO.")]
  public class CommentIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.CommentIntl;
    }
  }
