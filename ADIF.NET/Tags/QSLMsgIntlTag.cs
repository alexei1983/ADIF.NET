
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL card message.
  /// </summary>
  public class QSLMsgIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.QSLMsgIntl;

    /// <summary>
    /// 
    /// </summary>
    public QSLMsgIntlTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">QSL card message.</param>
    public QSLMsgIntlTag(string value) : base(value) { }
  }
}
