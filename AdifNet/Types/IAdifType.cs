
namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents an ADIF type.
    /// </summary>
    public interface IAdifType
    {
        /// <summary>
        /// <see cref="System.Type"/> of the ADIF type.
        /// </summary>
        Type UnderlyingType { get; }

        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        string? Type { get; }

        /// <summary>
        /// Name of the type.
        /// </summary>
        string? TypeName { get; }

        /// <summary>
        /// Format string for representing values of this type as a string.
        /// </summary>
        string? FormatString { get; }

        /// <summary>
        /// Is the type a range?
        /// </summary>
        bool IsRange { get; }

        /// <summary>
        /// Is the type an enumeration?
        /// </summary>
        bool IsEnumeration { get; }

        /// <summary>
        /// Does the type allow multiple values?
        /// </summary>
        bool MultiValue { get; }

        /// <summary>
        /// Format provider.
        /// </summary>
        IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Minimum value.
        /// </summary>
        double MinValue { get; }

        /// <summary>
        /// Maximum value.
        /// </summary>
        double MaxValue { get; }
    }
}
