using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// 
  /// </summary>
  public class DXCCException : ValueException {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    public DXCCException(string message, string value) : base(message, value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public DXCCException(string message) : base(message) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public DXCCException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    /// <param name="innerException"></param>
    public DXCCException(string message, string value, Exception innerException) : base(message, value, innerException) { }
  }
}
