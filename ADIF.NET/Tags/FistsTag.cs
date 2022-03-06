
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's FISTS CW Club member information.
  /// </summary>
  public class FISTSTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Fists;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 1;

    /// <summary>
    /// Creates a new FISTS tag.
    /// </summary>
    public FISTSTag() { }

    /// <summary>
    /// Creates a new FISTS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public FISTSTag(double value) : base(value) { }
  }
}
