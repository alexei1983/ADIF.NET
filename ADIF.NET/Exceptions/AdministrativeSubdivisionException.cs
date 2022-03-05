using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// Represents an exception related to primary or secondary administrative subdivisions.
  /// </summary>
  public class AdministrativeSubdivisionException : ValueException {

    /// <summary>
    /// Creates a new instance of the <see cref="AdministrativeSubdivisionException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="value">Value that caused the exception.</param>
    public AdministrativeSubdivisionException(string message, string value) : base(message, value) { }

    /// <summary>
    /// Creates a new instance of the <see cref="AdministrativeSubdivisionException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AdministrativeSubdivisionException(string message) : base(message) { }

    /// <summary>
    /// Creates a new instance of the <see cref="AdministrativeSubdivisionException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Inner exception.</param>
    public AdministrativeSubdivisionException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// Creates a new instance of the <see cref="AdministrativeSubdivisionException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="value">Value that caused the exception.</param>
    /// <param name="innerException">Inner exception.</param>
    public AdministrativeSubdivisionException(string message, string value, Exception innerException) : base(message, value, innerException) { }
  }
}
