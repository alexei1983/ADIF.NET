using System.Collections.Generic;
using System.Collections;

namespace ADIF.NET {

  /// <summary>
  /// Represents a collection of QSOs.
  /// </summary>
  public class ADIFQSOCollection : List<ADIFQSO>, IList<ADIFQSO>, ICollection<ADIFQSO>, 
                                   ICollection, IEnumerable, IEnumerable<ADIFQSO> {

  }
}
