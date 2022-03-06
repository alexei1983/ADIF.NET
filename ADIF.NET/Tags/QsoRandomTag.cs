
namespace ADIF.NET.Tags {

  /// <summary>
  /// Indicates whether the QSO was random or scheduled.
  /// </summary>
  public class QSORandomTag : BooleanTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.QSORandom;

    /// <summary>
    /// Creates a new QSO_RANDOM tag.
    /// </summary>
    public QSORandomTag() { }

    /// <summary>
    /// Creates a new QSO_RANDOM tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSORandomTag(bool value) : base(value) { }
  }
}
