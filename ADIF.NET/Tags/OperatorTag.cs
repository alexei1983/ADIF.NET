
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the callsign of the logging operator.
  /// </summary>
  public class OperatorTag : StringTag, ITag {

    public override string Name => TagNames.Operator;

    public OperatorTag() { }

    public OperatorTag(string value) : base(value) { }
    }
  }
