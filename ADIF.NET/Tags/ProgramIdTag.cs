
namespace ADIF.NET.Tags {

  /// <summary>
  /// Identifies the name of the logger, converter, or utility that created or processed the ADIF data set.
  /// </summary>
  public class ProgramIdTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.ProgramId;

    /// <summary>
    /// Whether or not the tag is a header tag.
    /// </summary>
    public override bool Header => true;

    /// <summary>
    /// Creates a new PROGRAMID tag.
    /// </summary>
    public ProgramIdTag() { }

    /// <summary>
    /// Creates a new PROGRAMID tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ProgramIdTag(string value)
    {
      SetValue(value);
    }
  }
}
