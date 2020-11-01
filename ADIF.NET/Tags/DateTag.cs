using System;
using System.Globalization;

namespace ADIF.NET.Tags {
  public class DateTag : Tag<DateTime> {

    public override string FormatString { get; set; } = Values.AdifDateFormat;

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        if (value is DateTime dateTime)
          return dateTime;
        else if (DateTime.TryParseExact(value.ToString() ?? string.Empty,
                                       Values.AdifDateFormat,
                                       FormatProvider,
                                       DateTimeStyles.AllowInnerWhite | 
                                       DateTimeStyles.AllowLeadingWhite | 
                                       DateTimeStyles.AllowTrailingWhite,
                                       out DateTime dateTimeParsed))
          return dateTimeParsed;
        else {
          try {
            var convertedDateTime = Convert.ToDateTime(value);
            return convertedDateTime;
            }
          catch {
            }
          }
        }

      return null;
      }
    }
  }
