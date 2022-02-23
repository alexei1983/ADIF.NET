
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class TxPwrTag : NumberTag, ITag {

    public override string Name => TagNames.TxPwr;

    public override double MinValue => 0;

    public override double MaxValue => double.MaxValue;

    public TxPwrTag() { }

    public TxPwrTag(double value) : base(value) { }
  }
}
