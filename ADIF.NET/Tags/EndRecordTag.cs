
namespace ADIF.NET.Tags {
  public class EndRecordTag : StringTag, ITag {

    public override string Name => TagNames.EndRecord;
    public override bool SuppressLength => true;

    public override void SetValue(object value) {
      }

    public override void SetValue(string value) {
      }
    }
  }
