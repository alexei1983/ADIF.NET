
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the description of the contacted station's equipment.
    /// </summary>
    public class RigTag : MultilineStringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Rig;

        /// <summary>
        /// Creates a new RIG tag.
        /// </summary>
        public RigTag() { }

        /// <summary>
        /// Creates a new RIG tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public RigTag(string value) : base(value) { }
    }
}
