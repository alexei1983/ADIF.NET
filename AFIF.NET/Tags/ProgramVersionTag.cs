using System;
using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  [DisplayName("")]
  public class ProgramVersionTag : Tag<Version>, ITag {

    public override string Name => TagNames.ProgramVersion;
    public override bool Header => true;

    public ProgramVersionTag() {
      }

    public ProgramVersionTag(Version value) {
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
