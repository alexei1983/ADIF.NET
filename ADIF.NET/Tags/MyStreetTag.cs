﻿
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's street.
  /// </summary>
  public class MyStreetTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyStreet;

    /// <summary>
    /// Creates a new MY_STREET tag.
    /// </summary>
    public MyStreetTag() { }

    /// <summary>
    /// Creates a new MY_STREET tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyStreetTag(string value) : base(value) { }
  }
}
