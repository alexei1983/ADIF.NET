
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's ARRL section.
  /// </summary>
  public class ARRLSectTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.ARRLSect;

    public override ADIFEnumeration Options => Values.ARRLSections;

    public ARRLSectTag() { }

    public ARRLSectTag(string value) : base(value) { }
  }
}
