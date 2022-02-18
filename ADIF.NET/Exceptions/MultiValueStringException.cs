using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// 
  /// </summary>
  public class MultiValueStringException : Exception {

    /// <summary>
    /// 
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    public MultiValueStringException(string message, string value) : base(message)
    {
      Value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    /// <param name="innerException"></param>
    public MultiValueStringException(string message, string value, Exception innerException) : base(message, innerException)
    {
      Value = value;
    }

  }
}
