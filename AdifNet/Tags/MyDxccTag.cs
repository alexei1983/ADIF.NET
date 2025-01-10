
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the logging station's country code.
    /// </summary>
    public class MyDXCCTag : RestrictedEnumerationTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyDxcc;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.CountryCodes;

        /// <summary>
        /// Creates a new MY_DXCC tag.
        /// </summary>
        public MyDXCCTag() { }

        /// <summary>
        /// Creates a new MY_DXCC tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyDXCCTag(string value) : base(value) { }
    }
}
