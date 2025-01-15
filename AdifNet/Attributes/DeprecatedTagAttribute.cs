
namespace org.goodspace.Data.Radio.Adif.Attributes
{
    /// <summary>
    /// Indicates whether or not an ADIF tag is deprecated.
    /// </summary>
    /// <remarks>
    /// Creates a new instance of the <see cref="DeprecatedTagAttribute"/> class.
    /// </remarks>
    /// <param name="deprecated">Whether or not the tag is deprecated.</param>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DeprecatedTagAttribute(bool deprecated) : Attribute
    {
        /// <summary>
        /// Whether or not the tag is deprecated.
        /// </summary>
        public bool Deprecated { get; set; } = deprecated;

        /// <summary>
        /// Name of an alternate ADIF tag that can be used instead of the deprecated tag.
        /// </summary>
        public string? AlternateTagName { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="DeprecatedTagAttribute"/> class.
        /// </summary>
        /// <param name="alternateTagName">Name of an alternate ADIF tag that can be used instead of the deprecated tag.</param>
        public DeprecatedTagAttribute(string alternateTagName) : this(true)
        {
            AlternateTagName = alternateTagName;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DeprecatedTagAttribute"/> class.
        /// </summary>
        public DeprecatedTagAttribute() : this(true) { }
    }
}
