using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// 
  /// </summary>
  public class ADXParseException : Exception {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ADXParseException(string message) : base(message) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ADXParseException(string message, Exception innerException) : base(message, innerException) { }

  }
}
