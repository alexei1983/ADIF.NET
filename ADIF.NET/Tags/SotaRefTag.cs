
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class SOTARefTag : StringTag, ITag {

    public override string Name => TagNames.SOTARef;

    public override IADIFType ADIFType => new ADIFSOTARef();

    public SOTARefTag() { }

    public SOTARefTag(string value) : base(value) { }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFSOTARef.TryParse(value.ToString(), out _);
      }
    }
  }
