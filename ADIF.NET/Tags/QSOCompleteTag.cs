
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QSOCompleteTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.QSOComplete;

    public override ADIFEnumeration Options => Values.QSOCompleteStatuses;

    public QSOCompleteTag() { }

    public QSOCompleteTag(string value) : base(value) { }
  }
}
