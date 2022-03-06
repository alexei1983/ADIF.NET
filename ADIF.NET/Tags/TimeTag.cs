using System;
using System.Globalization;

namespace ADIF.NET.Tags {

  /// <summary>
  /// An ADIF.NET tag whose value represents a time in the format HHMM or HHMMSS.
  /// </summary>
  public class TimeTag : DateTag, ITag {

    public override string FormatString { get; set; } = Values.ADIF_TIME_FORMAT_SHORT;

    public bool ConvertToUTC { get; }

    public TimeTag() { }

    public TimeTag(DateTime value) : base(value) { }

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        var final = DateTime.MinValue;

        if (value is DateTime dateTime)
          final = dateTime;
        else {
          var objStr = value.ToString();

          var formatString = objStr.Length > 4 ? Values.ADIF_TIME_FORMAT_LONG : Values.ADIF_TIME_FORMAT_SHORT;

          if (DateTime.TryParseExact(objStr,
                                     formatString,
                                     FormatProvider,
                                     DateTimeStyles.AllowInnerWhite | 
                                     DateTimeStyles.AllowLeadingWhite | 
                                     DateTimeStyles.AllowTrailingWhite | 
                                     DateTimeStyles.NoCurrentDateDefault,
                                     out DateTime dateTimeParsed))
            final = dateTimeParsed;
          }

        if (final != DateTime.MinValue && ConvertToUTC)
          final = final.ToUniversalTime();

        return final;
        }

      return null;
      }
    }
  }
