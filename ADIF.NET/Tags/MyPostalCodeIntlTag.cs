﻿
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's postal code.
  /// </summary>
  public class MyPostalCodeIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyPostalCodeIntl;

    /// <summary>
    /// Creates a new MY_POSTAL_CODE_INTL tag.
    /// </summary>
    public MyPostalCodeIntlTag() { }

    /// <summary>
    /// Creates a new MY_POSTAL_CODE_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyPostalCodeIntlTag(string value) : base(value) { }
  }
}
