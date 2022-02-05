
namespace ADIF.NET.Types {

  /// <summary>
  /// 
  /// </summary>
  public class ADIFString : ADIFType<string>, IADIFType {

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.String;
  }
}
