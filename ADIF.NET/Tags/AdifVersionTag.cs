using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the version of ADIF used to generate the data set.
  /// </summary>
  public class ADIFVersionTag : Tag<Version>, ITag {

    public override string Name => TagNames.ADIFVer;
    public override bool Header => true;

    public ADIFVersionTag() {
      }

    public ADIFVersionTag(Version value) {
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
