using System;
using System.Globalization;

namespace ADIF.NET.Tags {
  public class TimeTag : DateTag, ITag {

    public override string FormatString { get; set; } = Values.AdifTimeFormatShort;

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        if (value is DateTime dateTime)
          return dateTime;
        else {
          var objStr = value.ToString() ?? string.Empty;

          var formatString = objStr.Length > 4 ? Values.AdifTimeFormatLong : Values.AdifTimeFormatShort;

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
