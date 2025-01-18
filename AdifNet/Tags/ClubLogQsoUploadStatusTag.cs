
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the upload status of the QSO on the Club Log online service.
    /// </summary>
    public class ClubLogQsoUploadStatusTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.ClubLogQsoUploadStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.QsoUploadStatuses;

        /// <summary>
        /// Creates a new CLUBLOG_QSO_UPLOAD_STATUS tag.
        /// </summary>
        public ClubLogQsoUploadStatusTag() { }

        /// <summary>
        /// Creates a new CLUBLOG_QSO_UPLOAD_STATUS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public ClubLogQsoUploadStatusTag(string value) : base(value) { }
    }
}
