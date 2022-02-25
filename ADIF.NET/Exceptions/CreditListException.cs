using System;

namespace ADIF.NET.Exceptions {
  /// <summary>
  /// 
  /// </summary>
  public class CreditListException : Exception {

    /// <summary>
    /// 
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    public CreditListException(string message, string value) : base(message)
    {
      Value = value;
    }

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
    public CreditListException(string message, string value, Exception innerException) : base(message, innerException)
    {
      Value = value;
    }
  }
}
