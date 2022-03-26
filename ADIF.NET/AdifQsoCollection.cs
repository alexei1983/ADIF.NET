using System.Collections.Generic;
using System.Collections;
using System.Linq;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// Represents a collection of QSOs.
  /// </summary>
  public class ADIFQSOCollection : List<ADIFQSO>, IList<ADIFQSO>, ICollection<ADIFQSO>, 
                                   ICollection, IEnumerable, IEnumerable<ADIFQSO> {

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFQSOCollection"/> class.
    /// </summary>
    public ADIFQSOCollection() { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFQSOCollection"/> class.
    /// </summary>
    /// <param name="qsos">Array of <see cref="ADIFQSO"/> objects to add to the collection.</param>
    public ADIFQSOCollection(params ADIFQSO[] qsos)
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
      this.ForEach(qso =>
      {
        var appDefTags = qso.GetAppDefTags();

        if (appDefTags != null && appDefTags.Length > 0)
        {
          tags.AddRange(appDefTags);
        }
      });

      return tags.GroupBy(t => t.Name)
                 .Select(t => t.FirstOrDefault())
                 .ToArray();
    }
  }
}
