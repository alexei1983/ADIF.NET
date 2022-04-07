
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class MySigInfoTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MySigInfo;

    /// <summary>
    /// Creates a new MY_SIG_INFO tag.
    /// </summary>
    public MySigInfoTag() { }

    /// <summary>
    /// Creates a new MY_SIG_INFO tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MySigInfoTag(string value) : base(value) { }
    }
  }
