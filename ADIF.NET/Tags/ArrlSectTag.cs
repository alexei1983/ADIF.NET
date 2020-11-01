using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  [DisplayName("The contacted station's ARRL section.")]
  public class ArrlSectTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.ArrlSect;
    public override string[] Options => typeof(ArrlSection).GetValuesArray();
    }
  }
