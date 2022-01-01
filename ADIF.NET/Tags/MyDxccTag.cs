
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's country code.
  /// </summary>
  public class MyDxccTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.MyDXCC;

    public override ADIFEnumeration Options => Values.CountryCodes;
    }
  }
