
namespace ADIF.NET.Tags {

  /// <summary>
  /// Tag that marks the end of a QSO record in an ADIF data set.
  /// </summary>
  public class EndRecordTag : ValuelessTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.EndRecord;  
  }
}
