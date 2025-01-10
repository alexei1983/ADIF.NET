
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the logging station's postal code.
    /// </summary>
    public class MyPostalCodeTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyPostalCode;

        /// <summary>
        /// Creates a new MY_POSTAL_CODE tag.
        /// </summary>
        public MyPostalCodeTag() { }

        /// <summary>
        /// Creates a new MY_POSTAL_CODE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyPostalCodeTag(string value) : base(value) { }
    }
}
