using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// 
  /// </summary>
  public class DXCCException : Exception {

    /// <summary>
    /// 
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    public DXCCException(string message, string value) : base(message)
    {
      Value = value;
    }

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
    public DXCCException(string message, string value, Exception innerException) : base(message, innerException)
    {
      Value = value;
    }
  }
}
