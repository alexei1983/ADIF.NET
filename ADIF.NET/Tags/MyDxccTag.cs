using System.Linq;
using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's country code.
  /// </summary>
  [DisplayName("The logging station's country code.")]
  public class MyDxccTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.MyDxcc;

    public override string[] Options => Values.CountryCodes.Select(c => c.Code.ToString()).ToArray();
    }
  }
