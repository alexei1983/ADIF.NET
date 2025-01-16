
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date the QSO was last uploaded to the HRDLog.net online service.
    /// </summary>
    public class HrdLogQsoUploadDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.HrdLogQsoUploadDate;

        /// <summary>
        /// Creates a new HRDLOG_QSO_UPLOAD_DATE tag.
        /// </summary>
        public HrdLogQsoUploadDateTag() { }

        /// <summary>
        /// Creates a new HRDLOG_QSO_UPLOAD_DATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public HrdLogQsoUploadDateTag(DateTime value) : base(value) { }
    }
}
