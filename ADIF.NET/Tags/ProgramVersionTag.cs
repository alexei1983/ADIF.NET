using System;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Identifies the version of the logger, converter, or utility that created or processed the ADIF data set.
  /// </summary>
  public class ProgramVersionTag : Tag<Version>, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.ProgramVersion;

    /// <summary>
    /// Whether or not the tag is a header tag.
    /// </summary>
    public override bool Header => true;

    /// <summary>
    /// Creates a new PROGRAMVERSION tag.
    /// </summary>
    public ProgramVersionTag()
    {
    }

    /// <summary>
    /// Creates a new PROGRAMVERSION tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ProgramVersionTag(Version value)
    {
      base.SetValue(value);
    }

    /// <summary>
    /// Converts the specified object to the expected value type for the tag.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    public override object ConvertValue(object value)
    {
      if (!(value is null))
      {
        try
        {
          var version = Version.Parse(value.ToString());
          return version;
        }
        catch (Exception ex)
        {
          throw new ValueConversionException(value, Name, ex);
        }
      }

      return null;
    }
  }
}
