
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's antenna.
  /// </summary>
  public class MyAntennaTag : StringTag, ITag {

    public override string Name => TagNames.MyAntenna;

    public MyAntennaTag() { }

    public MyAntennaTag(string value) : base(value) { }
  }
}
