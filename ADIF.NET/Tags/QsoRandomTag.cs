
namespace ADIF.NET.Tags {

  /// <summary>
  /// Indicates whether the QSO was random or scheduled.
  /// </summary>
  public class QSORandomTag : BooleanTag, ITag {

    public override string Name => TagNames.QSORandom;

    public QSORandomTag() { }

    public QSORandomTag(bool value) : base(value) { }
  }
}
