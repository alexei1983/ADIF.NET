
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's FISTS CW Club member information.
  /// </summary>
  public class MyFISTSTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyFists;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 1;

    /// <summary>
    /// Creates a new MY_FISTS tag.
    /// </summary>
    public MyFISTSTag() { }

    /// <summary>
    /// Creates a new MY_FISTS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyFISTSTag(double value) : base(value) { }
  }
}
