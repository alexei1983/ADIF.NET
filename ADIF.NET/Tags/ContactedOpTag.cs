
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the callsign of the individual operating the contacted station.
  /// </summary>
  public class ContactedOpTag : StringTag, ITag {

    public override string Name => TagNames.ContactedOp;

    public ContactedOpTag() { }

    public ContactedOpTag(string value) : base(value) { }
  }
}
