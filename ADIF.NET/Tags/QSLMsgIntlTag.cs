
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL card message.
  /// </summary>
  public class QSLMsgIntlTag : IntlMultilineStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.QSLMsgIntl;

    /// <summary>
    /// Creates a new QSLMSG_INTL tag.
    /// </summary>
    public QSLMsgIntlTag() { }

    /// <summary>
    /// Creates a new QSLMSG_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLMsgIntlTag(string value) : base(value) { }
  }
}
