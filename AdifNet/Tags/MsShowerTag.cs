
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the name of the meteor shower in a meteor scatter QSO.
    /// </summary>
    public class MsShowerTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MsShower;

        /// <summary>
        /// Creates a new MS_SHOWER tag.
        /// </summary>
        public MsShowerTag() { }

        /// <summary>
        /// Creates a new MS_SHOWER tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MsShowerTag(string value) : base(value) { }
    }
}
