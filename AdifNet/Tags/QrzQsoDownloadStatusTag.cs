
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the status of the QSO download from the QRZ.COM online service.
    /// </summary>
    public class QrzQsoDownloadStatusTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QrzQsoDownloadStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.QsoDownloadStatuses;

        /// <summary>
        /// Creates a new QRZCOM_QSO_DOWNLOAD_STATUS tag.
        /// </summary>
        public QrzQsoDownloadStatusTag() { }

        /// <summary>
        /// Creates a new QRZCOM_QSO_DOWNLOAD_STATUS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QrzQsoDownloadStatusTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new QRZCOM_QSO_DOWNLOAD_STATUS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QrzQsoDownloadStatusTag(AdifEnumerationValue value) : base(value) { }
    }
}
