
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's FISTS CW Club member information.
  /// </summary>
  public class MyFISTSTag : PositiveIntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyFists;

    /// <summary>
    /// Creates a new MY_FISTS tag.
    /// </summary>
    public MyFISTSTag() { }

    /// <summary>
    /// Creates a new MY_FISTS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyFISTSTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new MY_FISTS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyFISTSTag(int value) : base(value) { }
  }
}
