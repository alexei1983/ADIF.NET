using System;
using System.Globalization;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date and time that the ADIF dataset was created.
  /// </summary>
  public class CreatedTimestampTag : Tag<DateTime>, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.CreatedTimestamp;

    /// <summary>
    /// Format string.
    /// </summary>
    public override string FormatString { get; set; } = $"{Values.ADIF_DATE_FORMAT} {Values.ADIF_TIME_FORMAT_LONG}";

    /// <summary>
    /// Whether or not the tag is a header tag.
    /// </summary>
    public override bool Header => true;

    /// <summary>
    /// Creates a new CREATED_TIMESTAMP tag.
    /// </summary>
    public CreatedTimestampTag() {
      }

    /// <summary>
    /// Creates a new CREATED_TIMESTAMP tag.
    /// </summary>
    /// <param name="created">Initial tag value.</param>
    public CreatedTimestampTag(DateTime created) {
      SetValue(created);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
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
