
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date the QSO was last uploaded to the Ham QTH online service.
    /// </summary>
    public class HamQthQsoUploadDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.HamQthQsoUploadDate;

        /// <summary>
        /// Creates a new HAMQTH_QSO_UPLOAD_DATE tag.
        /// </summary>
        public HamQthQsoUploadDateTag() { }

        /// <summary>
        /// Creates a new HAMQTH_QSO_UPLOAD_DATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public HamQthQsoUploadDateTag(DateTime value) : base(value) { }

    }
}
