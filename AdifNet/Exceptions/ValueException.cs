using System;

namespace org.goodspace.Data.Radio.Adif.Exceptions
{

    /// <summary>
    /// Represents an exception related to the validation or processing of a specific value or values.
    /// </summary>
    public class ValueException : Exception
    {

        /// <summary>
        /// Value that caused the exception.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ValueException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="value"></param>
        public ValueException(string message, string value) : base(message)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ValueException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public ValueException(string message) : base(message) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ValueException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ValueException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ValueException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="value"></param>
        /// <param name="innerException">Inner exception.</param>
        public ValueException(string message, string value, Exception innerException) : base(message, innerException)
        {
            Value = value;
        }
    }
}
