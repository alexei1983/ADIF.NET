
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL card message.
  /// </summary>
  public class QSLMsgTag : StringTag, ITag {

    public override string Name => TagNames.QSLMsg;

    public QSLMsgTag() { }

    public QSLMsgTag(string value) : base(value) { }
  }
}
