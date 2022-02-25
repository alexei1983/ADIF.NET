using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// 
  /// </summary>
  public class ADIFParseException : Exception {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ADIFParseException(string message) : base(message) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ADIFParseException(string message, Exception innerException) : base(message, innerException) { }

  }
}
