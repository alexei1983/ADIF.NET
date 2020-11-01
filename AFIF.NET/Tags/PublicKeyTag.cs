using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents a public encryption key.
  /// </summary>
  [DisplayName("Public encryption key.")]
  public class PublicKeyTag : StringTag, ITag {

    public override string Name => TagNames.PublicKey;

    public PublicKeyTag() { }

    public PublicKeyTag(string value) {
      SetValue(value);
      }
    }
  }
