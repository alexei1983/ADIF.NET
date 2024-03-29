﻿
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's callsign.
  /// </summary>
  public class CallTag : BaseCallSignTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.Call;

    /// <summary>
    /// Creates a new CALL tag.
    /// </summary>
    public CallTag() { }

    /// <summary>
    /// Creates a new CALL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public CallTag(string value) : base(value) { }
    }
  }
