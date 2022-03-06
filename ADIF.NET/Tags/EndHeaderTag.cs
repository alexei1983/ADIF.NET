
namespace ADIF.NET.Tags {

  /// <summary>
  /// Tag that marks the end of the header in an ADIF data set.
  /// </summary>
  public class EndHeaderTag : ValuelessTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.EndHeader;
  }
}
