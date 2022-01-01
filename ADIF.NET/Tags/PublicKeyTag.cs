
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents a public encryption key.
  /// </summary>
  public class PublicKeyTag : StringTag, ITag {

    public override string Name => TagNames.PublicKey;

    public PublicKeyTag() { }

    public PublicKeyTag(string value) {
      SetValue(value);
      }
    }
  }
