using System;
using System.Globalization;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents an ADIF tag that stores a date value.
    /// </summary>
    public class DateTag : Tag<DateTime>, ITag
    {

        /// <summary>
        /// 
        /// </summary>
        public override string? FormatString { get; set; } = Values.ADIF_DATE_FORMAT;

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType ADIFType => new AdifDate();

        /// <summary>
        /// Creates a new instance of the <see cref="DateTag"/> class.
        /// </summary>
        public DateTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="DateTag"/> class.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public DateTag(DateTime value) : base(value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override object? ConvertValue(object? value)
        {

            if (value is not null)
            {

                if (value is DateTime dateTime)
                    return dateTime;
                else if (DateTime.TryParseExact(value.ToString(),
                                               FormatString,
                                               FormatProvider,
                                               DateTimeStyles.AllowInnerWhite |
                                               DateTimeStyles.AllowLeadingWhite |
                                               DateTimeStyles.AllowTrailingWhite,
                                               out DateTime dateTimeParsed))
                    return dateTimeParsed;
                else
                {
                    try
                    {
                        var convertedDateTime = Convert.ToDateTime(value);
                        return convertedDateTime;
                    }
                    catch
                    {
                    }
                }
            }

            return null;
        }
    }
}
