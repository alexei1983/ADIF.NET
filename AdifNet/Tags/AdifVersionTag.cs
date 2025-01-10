using System;
using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the version of ADIF used to generate the data set.
    /// </summary>
    public class AdifVersionTag : Tag<Version>, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.AdifVer;

        /// <summary>
        /// Whether or not the tag is a header tag.
        /// </summary>
        public override bool Header => true;

        /// <summary>
        /// Creates a new ADIF_VER tag.
        /// </summary>
        public AdifVersionTag()
        {
        }

        /// <summary>
        /// Creates a new ADIF_VER tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public AdifVersionTag(Version value)
        {
            base.SetValue(value);
        }

        /// <summary>
        /// Converts the specified object to the expected value type for the tag.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        public override object? ConvertValue(object? value)
        {
            if (value is not null)
            {
                try
                {
                    var version = Version.Parse(value.ToString() ?? string.Empty);
                    return version;
                }
                catch (Exception ex)
                {
                    throw new ValueConversionException(value, Name, ex);
                }
            }

            return null;
        }
    }
}
