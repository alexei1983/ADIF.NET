using System;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the date the QSO was last uploaded to the QRZ.COM online service.
    /// </summary>
    public class QrzQsoUploadDateTag : DateTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QrzQsoUploadDate;

        /// <summary>
        /// Creates a new QRZCOM_QSO_UPLOAD_DATE tag.
        /// </summary>
        public QrzQsoUploadDateTag() { }

        /// <summary>
        /// Creates a new QRZCOM_QSO_UPLOAD_DATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QrzQsoUploadDateTag(DateTime value) : base(value) { }

    }
}
