
using System.Collections;
using org.goodspace.Data.Radio.Adif.Tags;

namespace org.goodspace.Data.Radio.Adif
{

    /// <summary>
    /// Represents a collection of QSOs.
    /// </summary>
    public class AdifQsoCollection : List<AdifQso>, IList<AdifQso>, ICollection<AdifQso>,
                                     ICollection, IEnumerable, IEnumerable<AdifQso>
    {

        /// <summary>
        /// Creates a new instance of the <see cref="AdifQsoCollection"/> class.
        /// </summary>
        public AdifQsoCollection() { }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifQsoCollection"/> class.
        /// </summary>
        /// <param name="qsos">Array of <see cref="AdifQso"/> objects to add to the collection.</param>
        public AdifQsoCollection(params AdifQso[] qsos)
        {
            if (qsos != null)
                AddRange(qsos);
        }

        /// <summary>
        /// Retrieves a distinct list of all application-defined tags in the current collection.
        /// </summary>
        public AppDefTag[] GetAppDefTags()
        {
            var tags = new List<AppDefTag>();
            ForEach(qso =>
            {
                var appDefTags = qso.GetAppDefTags();

                if (appDefTags != null && appDefTags.Length > 0)
                    tags.AddRange(appDefTags);
            });

            return tags.GroupBy(t => t.Name)
                       .Select(t => t.First()).ToArray();
        }
    }
}
