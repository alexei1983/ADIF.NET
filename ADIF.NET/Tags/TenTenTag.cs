
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class TenTenTag : NumberTag, ITag {

    public override string Name => TagNames.TenTen;

    public TenTenTag() { }

    public TenTenTag(double value) : base(value) { }
  }
}
