
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two US counties where the contacted station is located on a border between two counties.
  /// </summary>
  public class USACACountiesTag : StringTag, ITag {

    public override string Name => TagNames.USACACounties;

    public override string ValueSeparator => Values.COLON.ToString();

    public USACACountiesTag() { }

    public USACACountiesTag(string value) : base(value) { }


    public override bool ValidateValue(object value) {

      if (base.ValidateValue(value)) {

        return value.ToString().Contains(ValueSeparator);
        }

      return false;
      }

    public override void SetValue(string value) {

      if (value.Contains(ValueSeparator)) {
        base.SetValue(value);
        }
      }
    }
  }
