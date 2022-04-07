using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the time the QSO started.
  /// </summary>
  public class TimeOnTag : TimeTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.TimeOn;

    /// <summary>
    /// Creates a new TIME_ON tag.
    /// </summary>
    public TimeOnTag() { }

    /// <summary>
    /// Creates a new TIME_ON tag.
    /// </summary>
    /// <param name="value">Intial tag value.</param>
    public TimeOnTag(DateTime value) : base(value) { }
  }
}
