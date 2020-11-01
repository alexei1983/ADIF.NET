
namespace ADIF.NET.Tags {
  public class EndHeaderTag : StringTag, ITag {

    public override string Name => TagNames.EndHeader;
    public override bool SuppressLength => true;
    public override bool Header => true;

    public override void SetValue(object value) {
      }

    public override void SetValue(string value) {
      }
    }
  }
