﻿
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the callsign of the individual operating the contacted station.
  /// </summary>
  public class ContactedOpTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.ContactedOp;

    /// <summary>
    /// Creates a new CONTACTED_OP tag.
    /// </summary>
    public ContactedOpTag() { }

    /// <summary>
    /// Creates a new CONTACTED_OP tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ContactedOpTag(string value) : base(value) { }
  }
}
