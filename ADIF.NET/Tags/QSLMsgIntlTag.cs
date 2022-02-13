
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL card message.
  /// </summary>
  public class QSLMsgIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.QSLMsgIntl;

    public QSLMsgIntlTag() { }

    public QSLMsgIntlTag(string value) : base(value) { }
  }
}
