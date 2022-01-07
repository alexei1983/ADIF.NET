
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class MySOTARefTag : StringTag, ITag {

    public override string Name => TagNames.MySOTARef;

    public MySOTARefTag() { }

    public MySOTARefTag(string value) : base(value) { }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsSOTADesignator();
      }
    }
  }
