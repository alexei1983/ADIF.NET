
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL card message.
  /// </summary>
  public class QSLMsgTag : StringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.QSLMsg;

    /// <summary>
    /// 
    /// </summary>
    public QSLMsgTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">QSL card message.</param>
    public QSLMsgTag(string value) : base(value) { }
  }
}
