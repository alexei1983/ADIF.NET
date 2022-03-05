using System;

namespace ADIF.NET.Exceptions {

  /// <summary>
  /// 
  /// </summary>
  public class SponsoredAwardListException : ValueException {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="award"></param>
    public SponsoredAwardListException(string message, string award) : base(message, award) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public SponsoredAwardListException(string message) : base(message) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public SponsoredAwardListException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="award"></param>
    /// <param name="innerException"></param>
    public SponsoredAwardListException(string message, string award, Exception innerException) : base(message, award, innerException) { }
  }
}
