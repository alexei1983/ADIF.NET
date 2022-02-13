
namespace ADIF.NET.Types {

  /// <summary>
  /// 
  /// </summary>
  public class ADIFEnumerationType : ADIFString, IADIFType {

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.Enumeration;

    public override bool IsEnumeration => true;

  }
}
