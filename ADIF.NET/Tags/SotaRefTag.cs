
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class SOTARefTag : StringTag, ITag {

    public override string Name => TagNames.SOTARef;

    public SOTARefTag() { }

    public SOTARefTag(string value) : base(value) { }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsSOTADesignator();
      }
    }
  }
