using System;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the version of the program that generated the ADIF data.
  /// </summary>
  public class ProgramVersionTag : Tag<Version>, ITag {

    /// <summary>
    /// The name of the tag.
    /// </summary>
    public override string Name => TagNames.ProgramVersion;

    /// <summary>
    /// Whether or not the tag is a header tag.
    /// </summary>
    public override bool Header => true;

    /// <summary>
    /// Creates a new instance of the <see cref="ProgramVersionTag"/>.
    /// </summary>
    public ProgramVersionTag() {
      }

    /// <summary>
    /// Creates a new instance of the <see cref="ProgramVersionTag"/>.
    /// </summary>
    /// <param name="value">Version of the program.</param>
    public ProgramVersionTag(Version value) {
      base.SetValue(value);
      }

    /// <summary>
    /// Converts the specified object to the expected value type for the current tag.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override object ConvertValue(object value) {
      
      if (!(value is null)) {

        try {
          var version = new Version(value.ToString());
          return version;
          }
        catch (Exception ex) {
          throw new ValueConversionException(value, Name, ex);
          }
        }

      return null;
      }
    }
  }
