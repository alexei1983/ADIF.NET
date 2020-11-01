using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's FISTS CW Club member information.
  /// </summary>
  [DisplayName("The logging station's FISTS CW Club member information.")]
  public class MyFistsTag : StringTag, ITag {

    public override string Name => TagNames.MyFists;
    }
  }
