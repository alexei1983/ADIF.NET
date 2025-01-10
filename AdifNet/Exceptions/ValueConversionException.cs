using System;

namespace org.goodspace.Data.Radio.Adif.Exceptions
{

    /// <summary>
    /// 
    /// </summary>
    public class ValueConversionException : Exception
    {

        /// <summary>
        /// 
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? TagName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tagName"></param>
        public ValueConversionException(object? value, string tagName) : base($"Unable to convert value " +
                                                                             $"{(value == null ? string.Empty : value.ToString())} in tag " +
                                                                             $"{tagName ?? string.Empty}.")
        {
            Value = value;
            TagName = tagName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <param name="tagName"></param>
        public ValueConversionException(string message, object value, string tagName) : base(message)
        {
            Value = value;
            TagName = tagName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <param name="tagName"></param>
        /// <param name="innerException"></param>
        public ValueConversionException(string message, object value, string tagName, Exception innerException) : base(message, innerException)
        {
            Value = value;
            TagName = tagName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tagName"></param>
        /// <param name="innerException"></param>
        public ValueConversionException(object? value, string tagName, Exception innerException) : base($"Unable to convert value " +
                                                                                                       $"{(value == null ? string.Empty : value.ToString())} in tag " +
                                                                                                       $"{tagName ?? string.Empty}.", innerException)
        {
            Value = value;
            TagName = tagName;
        }

    }
}
