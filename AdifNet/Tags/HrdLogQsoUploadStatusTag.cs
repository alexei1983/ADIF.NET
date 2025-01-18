
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the upload status of the QSO on the HRDLog.net online service.
    /// </summary>
    public class HrdLogQsoUploadStatusTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.HrdLogQsoUploadStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.QsoUploadStatuses;

        /// <summary>
        /// Creates a new HRDLOG_QSO_UPLOAD_STATUS tag.
        /// </summary>
        public HrdLogQsoUploadStatusTag() { }

        /// <summary>
        /// Creates a new HRDLOG_QSO_UPLOAD_STATUS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public HrdLogQsoUploadStatusTag(string value) : base(value) { }
    }
}
