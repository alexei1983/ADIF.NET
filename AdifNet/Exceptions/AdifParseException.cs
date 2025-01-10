
namespace org.goodspace.Data.Radio.Adif.Exceptions {

  /// <summary>
  /// Represents an exception that occurred while parsing ADIF.
  /// </summary>
  public class AdifParseException : Exception {

    /// <summary>
    /// Creates a new instance of the <see cref="AdifParseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AdifParseException(string message) : base(message) { }

    /// <summary>
    /// Creates a new instance of the <see cref="AdifParseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Inner exception.</param>
    public AdifParseException(string message, Exception innerException) : base(message, innerException) { }

  }
}
