
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's Ten-Ten number.
  /// </summary>
  public class TenTenTag : PositiveIntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.TenTen;

    /// <summary>
    /// Creates a new TEN_TEN tag.
    /// </summary>
    public TenTenTag() { }

    /// <summary>
    /// Creates a new TEN_TEN tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public TenTenTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new TEN_TEN tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public TenTenTag(int value) : base(value) { }
  }
}
