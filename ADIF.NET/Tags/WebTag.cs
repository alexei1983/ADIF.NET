
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's URL.
  /// </summary>
  public class WebTag : StringTag, ITag {

    public override string Name => TagNames.Web;

    public WebTag() { }

    public WebTag(string value) : base(value) { }
  }
}
