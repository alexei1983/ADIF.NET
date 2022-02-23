
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's UKSMG (UK Six Metre Group) member number.
  /// </summary>
  public class UKSMGTag : NumberTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.UKSMG;

    /// <summary>
    /// 
    /// </summary>
    public override double MinValue => 0;

    /// <summary>
    /// 
    /// </summary>
    public override double MaxValue => ADIFType.MaxValue;

    /// <summary>
    /// 
    /// </summary>
    public UKSMGTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public UKSMGTag(double value) : base(value) { }

  }
}
