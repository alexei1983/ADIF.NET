using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date the QSO was last uploaded to the QRZ.COM online service.
  /// </summary>
  public class QRZQSOUploadDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.QRZQSOUploadDate;

    /// <summary>
    /// Creates a new QRZCOM_QSO_UPLOAD_DATE tag.
    /// </summary>
    public QRZQSOUploadDateTag() { }

    /// <summary>
    /// Creates a new QRZCOM_QSO_UPLOAD_DATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QRZQSOUploadDateTag(DateTime value) : base(value) { }

    }
  }
