using System;

namespace org.goodspace.Data.Radio.Adif.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class DxccException : ValueException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="value"></param>
        public DxccException(string message, string value) : base(message, value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DxccException(string message) : base(message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DxccException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <param name="innerException"></param>
        public DxccException(string message, string value, Exception innerException) : base(message, value, innerException) { }
    }
}
