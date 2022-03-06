
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the callsign of the logging operator.
  /// </summary>
  public class OperatorTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Operator;

    /// <summary>
    /// Creates a new OPERATOR tag.
    /// </summary>
    public OperatorTag() { }

    /// <summary>
    /// Creates a new OPERATOR tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public OperatorTag(string value) : base(value) { }
  }
}
