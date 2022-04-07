
namespace ADIF.NET.Tags {

  /// <summary>
  /// Indicates whether or not the QSO pertains to a shortwave listener (SWL) report.
  /// </summary>
  public class SWLTag : BooleanTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.SWL;

    /// <summary>
    /// Creates a new SWL tag.
    /// </summary>
    public SWLTag() { }

    /// <summary>
    /// Creates a new SWL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public SWLTag(bool value) : base(value) { }
  }
}
