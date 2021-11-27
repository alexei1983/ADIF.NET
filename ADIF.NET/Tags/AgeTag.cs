using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's operator's age in years.
  /// </summary>
  [DisplayName("The contacted station's operator's age in years.")]
  public class AgeTag : NumberTag, ITag {

    public override double MinValue => 1;

    public override double MaxValue => 120;

    public override string Name => TagNames.Age;
    }
  }
