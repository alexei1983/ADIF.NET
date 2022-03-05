using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// Represents an exception that occurred while parsing ADX.
  /// </summary>
  public class ADXParseException : Exception {

    /// <summary>
    /// Creates a new instance of the <see cref="ADXParseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public ADXParseException(string message) : base(message) { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADXParseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Inner exception.</param>
    public ADXParseException(string message, Exception innerException) : base(message, innerException) { }

  }
}
