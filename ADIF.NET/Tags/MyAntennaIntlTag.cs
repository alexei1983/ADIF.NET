
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's antenna.
  /// </summary>
  public class MyAntennaIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyAntennaIntl;

    /// <summary>
    /// Creates a new MY_ANTENNA_INTL tag.
    /// </summary>
    public MyAntennaIntlTag() { }

    /// <summary>
    /// Creates a new MY_ANTENNA_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyAntennaIntlTag(string value) : base(value) { }
  }
}
