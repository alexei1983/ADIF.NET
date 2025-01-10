
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contacted station's ARRL section.
    /// </summary>
    public class ARRLSectTag : RestrictedEnumerationTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.ArrlSect;

        /// <summary>
        /// 
        /// </summary>
        public override AdifEnumeration Options => Values.ArrlSections;

        /// <summary>
        /// Creates a new ARRL_SECT tag.
        /// </summary>
        public ARRLSectTag() { }

        /// <summary>
        /// Creates a new ARRL_SECT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public ARRLSectTag(string value) : base(value) { }
    }
}
