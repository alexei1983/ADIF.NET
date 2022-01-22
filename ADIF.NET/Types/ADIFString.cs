
namespace ADIF.NET.Types {
  public class ADIFString : ADIFType<string>, IADIFType {

    public override string Type => DataTypes.String;
  }
}
