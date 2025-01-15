using System.Globalization;

namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents an ADIF data type.
    /// </summary>
    /// <typeparam name="T">The <seealso cref="System.Type"/> of the ADIF type's underlying value.</typeparam>
    public abstract class AdifType<T> : IAdifType
    {
        /// <summary>
        /// Default ADIF type.
        /// </summary>
        public static readonly IAdifType Default = new AdifString();

        /// <summary>
        /// The expected <seealso cref="System.Type"/> of the ADIF type's underlying value.
        /// </summary>
        public virtual Type UnderlyingType => typeof(T);

        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public virtual string? Type { get; }

        /// <summary>
        /// ADIF type name.
        /// </summary>
        public virtual string? TypeName { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string? FormatString { get; }

        /// <summary>
        /// Whether or not the type is a range.
        /// </summary>
        public virtual bool IsRange { get; }

        /// <summary>
        /// Whether or not the type is an enumeration.
        /// </summary>
        public virtual bool IsEnumeration { get; }

        /// <summary>
        /// Whether or not the type is multivalued.
        /// </summary>
        public virtual bool MultiValue { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IFormatProvider FormatProvider { get; } = CultureInfo.CurrentCulture;

        /// <summary>
        /// Minimum numeric value for the type.
        /// </summary>
        public virtual double MinValue { get; }

        /// <summary>
        /// Maximum numeric value for the type.
        /// </summary>
        public virtual double MaxValue { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public virtual bool IsValidValue(object? o)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public virtual bool IsValidValue(string? s)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public abstract T Parse(string? s);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool TryParse(string? s, out T? value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        object IAdifType.Parse(string? s)
        {
            return Parse(s) ?? new object();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IAdifType.TryParse(string? s, out object? value)
        {
            var result = TryParse(s, out T? _value);
            value = _value;
            return result;
        }
    }
}
