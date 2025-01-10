
namespace org.goodspace.Data.Radio.Adif.Exceptions
{
    /// <summary>
    /// Represents an exception related to an application-defined field or its attributes or values.
    /// </summary>
    public class AppDefTagException : Exception
    {
        /// <summary>
        /// Name of the application-defined field that caused the exception.
        /// </summary>
        public string? FieldName { get; }

        /// <summary>
        /// Value of the application-defined field that caused the exception.
        /// </summary>
        public object? Value { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="AppDefTagException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="fieldName">Name of the application-defined field that caused the exception.</param>
        public AppDefTagException(string message, string fieldName) : base(message)
        {
            FieldName = fieldName;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AppDefTagException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="fieldName">Name of the application-defined field that caused the exception.</param>
        /// <param name="value">Value of the application-defined field that caused the exception.</param>
        public AppDefTagException(string message, string fieldName, object value) : base(message)
        {
            FieldName = fieldName;
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AppDefTagException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public AppDefTagException(string message) : base(message) { }

        /// <summary>
        /// Creates a new instance of the <see cref="AppDefTagException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public AppDefTagException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Creates a new instance of the <see cref="AppDefTagException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="value">Field value.</param>
        /// <param name="fieldName">Field name.</param>
        public AppDefTagException(string message, string fieldName, object value, Exception innerException) : base(message, innerException)
        {
            FieldName = fieldName;
            Value = value;
        }
    }
}
