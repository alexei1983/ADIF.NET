using System;
using System.Globalization;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date and time that the ADIF file was created.
  /// </summary>
  public class CreatedTimestampTag : Tag<DateTime>, ITag {

    public override string Name => TagNames.CreatedTimestamp;
    public override string FormatString { get; set; } = $"{Values.ADIF_DATE_FORMAT} {Values.ADIF_TIME_FORMAT_LONG}";
    public override bool Header => true;

    public CreatedTimestampTag() {
      }

    public CreatedTimestampTag(DateTime created) {
      SetValue(created);
      }

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        if (DateTime.TryParseExact(value.ToString(),
                                   FormatString ?? $"{Values.ADIF_DATE_FORMAT} {Values.ADIF_TIME_FORMAT_LONG}",
                                   FormatProvider,
                                   DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite,
                                   out DateTime dateTimeParsed))
          return dateTimeParsed;
        }

      return null;
      }

    }
  }
