using System;
using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the version of ADIF used to create the file.
  /// </summary>
  [DisplayName("The version of ADIF used to create the file.")]
  public class AdifVersionTag : Tag<Version>, ITag {

    public override string Name => TagNames.AdifVer;
    public override bool Header => true;

    public AdifVersionTag() {
      }

    public AdifVersionTag(Version value) {
      base.SetValue(value);
      }

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        try {
          var version = new Version(value.ToString());
          return version;
          }
        catch {
          }
        }

      return null;
      }
    }
  }
