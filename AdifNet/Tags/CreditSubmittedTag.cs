
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the list of credits sought for the QSO.
    /// </summary>
    public class CreditSubmittedTag : CreditListTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.CreditSubmitted;

        /// <summary>
        /// Creates a new CREDIT_SUBMITTED tag.
        /// </summary>
        public CreditSubmittedTag() { }

        /// <summary>
        /// Creates a new CREDIT_SUBMITTED tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public CreditSubmittedTag(string value) : base(value) { }
    }
}
