
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date the QSO was downloaded from the QRZ.COM online service.
    /// </summary>
    public class QrzQsoDownloadDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QrzQsoDownloadDate;

        /// <summary>
        /// Creates a new QRZCOM_QSO_DOWNLOAD_DATE tag.
        /// </summary>
        public QrzQsoDownloadDateTag() { }

        /// <summary>
        /// Creates a new QRZCOM_QSO_DOWNLOAD_DATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QrzQsoDownloadDateTag(DateTime value) : base(value) { }
    }
}
