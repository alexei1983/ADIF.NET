
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the upload status of the QSO on the Ham QTH online service.
    /// </summary>
    public class HamQthQsoUploadStatusTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.HamQthQsoUploadStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.QsoUploadStatuses;

        /// <summary>
        /// Creates a new HAMQTH_QSO_UPLOAD_STATUS tag.
        /// </summary>
        public HamQthQsoUploadStatusTag() { }

        /// <summary>
        /// Creates a new HAMQTH_QSO_UPLOAD_STATUS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public HamQthQsoUploadStatusTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new HAMQTH_QSO_UPLOAD_STATUS tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public HamQthQsoUploadStatusTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
