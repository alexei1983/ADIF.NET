using System;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the date on which the QSO started.
    /// </summary>
    public class QsoDateTag : DateTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QsoDate;

        /// <summary>
        /// Creates a new QSO_DATE tag.
        /// </summary>
        public QsoDateTag() { }

        /// <summary>
        /// Creates a new QSO_DATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QsoDateTag(DateTime value) : base(value) { }

    }
}
