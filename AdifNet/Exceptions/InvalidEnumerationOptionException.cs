
namespace org.goodspace.Data.Radio.Adif.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class InvalidEnumerationOptionException : ValueException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="value"></param>
        public InvalidEnumerationOptionException(string message, string value) : base(message, value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public InvalidEnumerationOptionException(string message) : base(message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InvalidEnumerationOptionException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <param name="innerException"></param>
        public InvalidEnumerationOptionException(string message, string value, Exception innerException) : base(message, value, innerException) { }
    }
}
