using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest class (e.g. for ARRL Field Day).
  /// </summary>
  [DisplayName("Contest class (e.g. for ARRL Field Day)")]
  public class ClassTag : StringTag, ITag {

    public override string Name => TagNames.Class;
    }
  }
