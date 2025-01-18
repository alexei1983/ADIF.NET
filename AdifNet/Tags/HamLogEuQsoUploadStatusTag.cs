
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the upload status of the QSO on the HamLog online service.
    /// </summary>
    public class HamLogEuQsoUploadStatusTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.HamLogEuQsoUploadStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.QsoUploadStatuses;

        /// <summary>
        /// Creates a new HAMLOGEU_QSO_UPLOAD_STATUS tag.
        /// </summary>
        public HamLogEuQsoUploadStatusTag() { }

        /// <summary>
        /// Creates a new HAMLOGEU_QSO_UPLOAD_STATUS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public HamLogEuQsoUploadStatusTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new HAMLOGEU_QSO_UPLOAD_STATUS tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public HamLogEuQsoUploadStatusTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
