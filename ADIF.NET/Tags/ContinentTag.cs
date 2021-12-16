
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the continent of the contacted station.
  /// </summary>
  public class ContinentTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.Continent;

    public override ADIFEnumeration Options => Values.Continents;
    }
  }
