using System;
using System.Globalization;

namespace ADIF.NET.Tags {
  public class CreatedTimestampTag : Tag<DateTime>, ITag {

    public override string Name => TagNames.CreatedTimestamp;
    public override string FormatString { get; set; } = $"{Values.AdifDateFormat} {Values.AdifTimeFormatLong}";
    public override bool Header => true;

    public CreatedTimestampTag() {
      }

    public CreatedTimestampTag(DateTime created) {
      SetValue(created);
      }

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        if (DateTime.TryParseExact(value.ToString() ?? string.Empty,
                                   FormatString ?? $"{Values.AdifDateFormat} {Values.AdifTimeFormatLong}",
                                   FormatProvider,
                                   DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite,
                                   out DateTime dateTimeParsed))
          return dateTimeParsed;
        }

      return null;
      }

    }
  }
