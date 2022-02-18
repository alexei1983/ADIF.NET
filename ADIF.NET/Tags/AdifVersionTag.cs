using System;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the version of ADIF used to generate the data set.
  /// </summary>
  public class ADIFVersionTag : Tag<Version>, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.ADIFVer;

    /// <summary>
    /// 
    /// </summary>
    public override bool Header => true;

    /// <summary>
    /// 
    /// </summary>
    public ADIFVersionTag()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public ADIFVersionTag(Version value)
    {
      base.SetValue(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {

      if (!(value is null))
      {
        try
        {
          var version = new Version(value.ToString());
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
