using System;

namespace org.goodspace.Data.Radio.Adif.Exceptions {

  /// <summary>
  /// Represents an exception that occurred while parsing ADX.
  /// </summary>
  public class AdxParseException : Exception {

    /// <summary>
    /// Creates a new instance of the <see cref="AdxParseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AdxParseException(string message) : base(message) { }

    /// <summary>
    /// Creates a new instance of the <see cref="AdxParseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Inner exception.</param>
    public AdxParseException(string message, Exception innerException) : base(message, innerException) { }

  }
}
