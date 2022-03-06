﻿
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the mode of the QSO.
  /// </summary>
  public class SubmodeTag : EnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Submode;

    /// <summary>
    /// Valid enumeration options.
    /// </summary>
    public override ADIFEnumeration Options => Values.Modes;

    /// <summary>
    /// Creates a new SUBMODE tag.
    /// </summary>
    public SubmodeTag() { }

    /// <summary>
    /// Creates a new SUBMODE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public SubmodeTag(string value) : base(value) { }
  }
}
