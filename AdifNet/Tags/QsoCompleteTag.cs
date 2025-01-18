
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Indicates whether the QSO was complete from the perspective of the logging station.
    /// </summary>
    public class QsoCompleteTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QsoComplete;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.QsoCompleteStatuses;

        /// <summary>
        /// Creates a new QSO_COMPLETE tag.
        /// </summary>
        public QsoCompleteTag() { }

        /// <summary>
        /// Creates a new QSO_COMPLETE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QsoCompleteTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new QSO_COMPLETE tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public QsoCompleteTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
