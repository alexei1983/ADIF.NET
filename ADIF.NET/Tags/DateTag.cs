using System;
using System.Globalization;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF tag that stores a date value.
  /// </summary>
  public class DateTag : Tag<DateTime> {

    public override string FormatString { get; set; } = Values.ADIF_DATE_FORMAT;

    public DateTag() { }

    public DateTag(DateTime value) : base(value) { }

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        if (value is DateTime dateTime)
          return dateTime;
        else if (DateTime.TryParseExact(value.ToString(),
                                       Values.ADIF_DATE_FORMAT,
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
