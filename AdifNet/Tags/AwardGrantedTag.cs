
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the list of awards granted by a sponsor.
    /// </summary>
    public class AwardGrantedTag : SponsoredAwardListTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.AwardGranted;

        /// <summary>
        /// Creates a new AWARD_GRANTED tag.
        /// </summary>
        public AwardGrantedTag() { }

        /// <summary>
        /// Creates a new AWARD_GRANTED tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public AwardGrantedTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new AWARD_GRANTED tag.
        /// </summary>
        /// <param name="values">Initial tag values.</param>
        public AwardGrantedTag(params string[] values) : base(values) { }
    }
}
