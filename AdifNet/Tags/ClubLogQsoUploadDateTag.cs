

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date the QSO was last uploaded to the Club Log online service.
    /// </summary>
    public class ClubLogQsoUploadDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.ClubLogQsoUploadDate;

        /// <summary>
        /// Creates a new CLUBLOG_QSO_UPLOAD_DATE tag.
        /// </summary>
        public ClubLogQsoUploadDateTag() { }

        /// <summary>
        /// Creates a new CLUBLOG_QSO_UPLOAD_DATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public ClubLogQsoUploadDateTag(DateTime value) : base(value) { }
    }
}
