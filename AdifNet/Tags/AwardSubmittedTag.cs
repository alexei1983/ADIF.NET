
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the list of awards submitted to a sponsor.
    /// </summary>
    public class AwardSubmittedTag : SponsoredAwardListTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.AwardSubmitted;

        /// <summary>
        /// Creates a new AWARD_SUBMITTED tag.
        /// </summary>
        public AwardSubmittedTag() { }

        /// <summary>
        /// Creates a new AWARD_SUBMITTED tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public AwardSubmittedTag(string value) : base(value) { }
    }
}
