using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's country code.
  /// </summary>
  [DisplayName("The contacted station's country code.")]
  public class DxccTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.Dxcc;

    public override string[] Options => null;
    }
  }
