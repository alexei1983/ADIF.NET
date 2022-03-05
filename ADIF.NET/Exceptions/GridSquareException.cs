using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// 
  /// </summary>
  public class GridSquareException : ValueException {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    public GridSquareException(string message, string value) : base(message, value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public GridSquareException(string message) : base(message) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public GridSquareException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="value"></param>
    /// <param name="innerException"></param>
    public GridSquareException(string message, string value, Exception innerException) : base(message, value, innerException) { }
  }
}
