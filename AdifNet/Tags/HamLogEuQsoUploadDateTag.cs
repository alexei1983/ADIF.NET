
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date the QSO was last uploaded to the HamLog online service.
    /// </summary>
    public class HamLogEuQsoUploadDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.HamLogEuQsoUploadDate;

        /// <summary>
        /// Creates a new HAMLOGEU_QSO_UPLOAD_DATE tag.
        /// </summary>
        public HamLogEuQsoUploadDateTag() { }

        /// <summary>
        /// Creates a new HAMLOGEU_QSO_UPLOAD_DATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public HamLogEuQsoUploadDateTag(DateTime value) : base(value) { }

    }
}
