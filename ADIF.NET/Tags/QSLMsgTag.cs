
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL card message.
  /// </summary>
  public class QSLMsgTag : MultilineStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QSLMsg;

    /// <summary>
    /// Creates a new QSLMSG tag.
    /// </summary>
    public QSLMsgTag() { }

    /// <summary>
    /// Creates a new QSLMSG tag.
    /// </summary>
    /// <param name="value">QSL card message.</param>
    public QSLMsgTag(string value) : base(value) { }
  }
}
