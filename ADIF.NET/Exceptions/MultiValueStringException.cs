using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// 
  /// </summary>
  public class MultiValueStringException : ValueException {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    public MultiValueStringException(string message, string value) : base(message, value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    /// <param name="innerException"></param>
    public MultiValueStringException(string message, string value, Exception innerException) : base(message, value, innerException) { }

  }
}
