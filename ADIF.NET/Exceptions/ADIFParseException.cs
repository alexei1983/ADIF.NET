using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// Represents an exception that occurred while parsing ADIF.
  /// </summary>
  public class ADIFParseException : Exception {

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFParseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public ADIFParseException(string message) : base(message) { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFParseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Inner exception.</param>
    public ADIFParseException(string message, Exception innerException) : base(message, innerException) { }

  }
}
