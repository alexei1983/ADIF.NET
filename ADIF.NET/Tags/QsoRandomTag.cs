
namespace ADIF.NET.Tags {

  /// <summary>
  /// Indicates whether the QSO was random or scheduled.
  /// </summary>
  public class QSORandomTag : BooleanTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.QSORandom;

    /// <summary>
    /// 
    /// </summary>
    public QSORandomTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public QSORandomTag(bool value) : base(value) { }
  }
}
