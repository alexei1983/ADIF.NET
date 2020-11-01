using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's FISTS CW Club Century Certificate (CC) number.
  /// </summary>
  [DisplayName("The contacted station's FISTS CW Club Century Certificate (CC) number.")]
  public class FistsCcTag : StringTag, ITag {

    public override string Name => TagNames.FistsCc;
    }
  }
