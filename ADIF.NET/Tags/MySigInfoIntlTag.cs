
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class MySigInfoIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MySigInfoIntl;

    /// <summary>
    /// Creates a new MY_SIG_INFO_INTL tag.
    /// </summary>
    public MySigInfoIntlTag() { }

    /// <summary>
    /// Creates a new MY_SIG_INFO_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MySigInfoIntlTag(string value) : base(value) { }
  }
}
