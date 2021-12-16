using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  [DisplayName("The contacted station's ARRL section.")]
  public class ARRLSectTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.ARRLSect;
    public override ADIFEnumeration Options => Values.ARRLSections;
    }
  }
