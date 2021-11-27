using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Indicates whether or not the QSO pertains to a shortwave listener (SWL) report.
  /// </summary>
  [DisplayName("Indicates that the QSO information pertains to an SWL report.")]
  public class SWLTag : BooleanTag, ITag {

    public override string Name => TagNames.SWL;
    }
  }
