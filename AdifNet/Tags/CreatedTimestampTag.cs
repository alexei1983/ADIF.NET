﻿using System.Globalization;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date and time that the ADIF dataset was created.
    /// </summary>
    public class CreatedTimestampTag : Tag<DateTime>, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.CreatedTimestamp;

        /// <summary>
        /// Format string.
        /// </summary>
        public override string? FormatString { get; set; } = $"{AdifConstants.DateFormat} {AdifConstants.TimeFormatLong}";

        /// <summary>
        /// Whether or not the tag is a header tag.
        /// </summary>
        public override bool Header => true;

        /// <summary>
        /// Creates a new CREATED_TIMESTAMP tag.
        /// </summary>
        public CreatedTimestampTag()
        {
        }

        /// <summary>
        /// Creates a new CREATED_TIMESTAMP tag.
        /// </summary>
        /// <param name="created">Initial tag value.</param>
        public CreatedTimestampTag(DateTime created)
        {
            SetValue(created);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override object? ConvertValue(object? value)
        {
            if (value is not null)
            {
                if (value is DateTime dateTimeIn)
                    return dateTimeIn;
                else if (DateTime.TryParseExact(value is string strVal ? strVal : value.ToString(),
                                               FormatString ?? $"{AdifConstants.DateFormat} {AdifConstants.TimeFormatLong}",
                                               FormatProvider,
                                               DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite,
                                               out DateTime dateTimeParsed))
                    return dateTimeParsed;
            }

            return null;
        }
    }
}
