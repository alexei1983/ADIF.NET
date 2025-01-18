
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's ARRL section.
    /// </summary>
    public class ArrlSectTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.ArrlSect;

        /// <summary>
        /// 
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.ArrlSections;

        /// <summary>
        /// Creates a new ARRL_SECT tag.
        /// </summary>
        public ArrlSectTag() { }

        /// <summary>
        /// Creates a new ARRL_SECT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public ArrlSectTag(string value) : base(value) { }
    }
}
