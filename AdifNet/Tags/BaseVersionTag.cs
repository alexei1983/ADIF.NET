using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// ADIF tag that stores a version number (e.g. 3.1.4)
    /// </summary>
    public class BaseVersionTag : Tag<Version>, ITag
    {
        /// <summary>
        /// Creates a new version tag.
        /// </summary>
        public BaseVersionTag()
        {
        }

        /// <summary>
        /// Creates a new version tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public BaseVersionTag(Version value)
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
                if (value is Version versionIn)
                    return versionIn;
                try
                {
                    var versionStr = value is string strVersion ? strVersion : value.ToString();
                    var version = Version.Parse(versionStr ?? string.Empty);
                    return version;
                }
                catch (Exception ex)
                {
                    throw new ValueConversionException(value, Name, ex);
                }
            }
            return default;
        }
    }
}
