using System;

namespace org.goodspace.Data.Radio.Adif.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditListException : ValueException
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="value"></param>
        public CreditListException(string message, string value) : base(message, value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public CreditListException(string message) : base(message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CreditListException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <param name="innerException"></param>
        public CreditListException(string message, string value, Exception innerException) : base(message, value, innerException) { }
    }
}
