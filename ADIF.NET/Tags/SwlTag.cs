
namespace ADIF.NET.Tags {

  /// <summary>
  /// Indicates whether or not the QSO pertains to a shortwave listener (SWL) report.
  /// </summary>
  public class SWLTag : BooleanTag, ITag {

    public override string Name => TagNames.SWL;

    public SWLTag() { }

    public SWLTag(bool value) : base(value) { }
    }
  }
