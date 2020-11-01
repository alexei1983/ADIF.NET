using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's URL.
  /// </summary>
  [DisplayName("The contacted station's URL.")]
  public class WebTag : StringTag, ITag {

    public override string Name => TagNames.Web;
    }
  }
