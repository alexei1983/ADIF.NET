
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the upload status of the QSO on the QRZ.COM online service.
    /// </summary>
    public class QrzQsoUploadStatusTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QrzQsoUploadStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.QsoUploadStatuses;

        /// <summary>
        /// Creates a new QRZCOM_QSO_UPLOAD_STATUS tag.
        /// </summary>
        public QrzQsoUploadStatusTag() { }

        /// <summary>
        /// Creates a new QRZCOM_QSO_UPLOAD_STATUS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QrzQsoUploadStatusTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new QRZCOM_QSO_UPLOAD_STATUS tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public QrzQsoUploadStatusTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
