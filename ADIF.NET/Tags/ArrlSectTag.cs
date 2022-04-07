
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's ARRL section.
  /// </summary>
  public class ARRLSectTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.ARRLSect;

    /// <summary>
    /// 
    /// </summary>
    public override ADIFEnumeration Options => Values.ARRLSections;

    /// <summary>
    /// Creates a new ARRL_SECT tag.
    /// </summary>
    public ARRLSectTag() { }

    /// <summary>
    /// Creates a new ARRL_SECT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ARRLSectTag(string value) : base(value) { }
  }
}
