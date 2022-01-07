using System;
using System.Globalization;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class TimeTag : DateTag, ITag {

    public override string FormatString { get; set; } = Values.ADIF_TIME_FORMAT_SHORT;

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        if (value is DateTime dateTime)
          return dateTime;
        else {
          var objStr = value.ToString() ?? string.Empty;

          var formatString = objStr.Length > 4 ? Values.ADIF_TIME_FORMAT_LONG : Values.ADIF_TIME_FORMAT_SHORT;

          if (DateTime.TryParseExact(objStr,
                                     formatString,
                                     FormatProvider,
                                     DateTimeStyles.AllowInnerWhite | 
                                     DateTimeStyles.AllowLeadingWhite | 
                                     DateTimeStyles.AllowTrailingWhite | 
                                     DateTimeStyles.NoCurrentDateDefault,
                                     out DateTime dateTimeParsed))
            return dateTimeParsed;
          }
        }

      return null;
      }

    }
  }
