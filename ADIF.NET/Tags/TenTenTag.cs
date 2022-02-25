
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class TenTenTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.TenTen;

    /// <summary>
    /// Creates a new TEN_TEN tag.
    /// </summary>
    public TenTenTag() { }

    /// <summary>
    /// Creates a new TEN_TEN tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public TenTenTag(double value) : base(value) { }
  }
}
