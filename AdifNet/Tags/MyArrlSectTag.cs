
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's ARRL section.
    /// </summary>
    public class MyArrlSectTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyArrlSect;

        /// <summary>
        /// 
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.ArrlSections;

        /// <summary>
        /// Creates a new MY_ARRL_SECT tag.
        /// </summary>
        public MyArrlSectTag() { }

        /// <summary>
        /// Creates a new MY_ARRL_SECT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyArrlSectTag(string value) : base(value) { }
    }
}
