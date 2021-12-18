using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two US counties where the contacted station is located on a border between two counties.
  /// </summary>
  [DisplayName("Two US counties in the case where the contacted station is located on a border between two counties, representing counties credited to the QSO for the CQ Magazine USA-CA award program.")]
  public class USACACountiesTag : StringTag, ITag {

    public override string Name => TagNames.USACACounties;

    public override string ValueSeparator => Values.COLON.ToString();

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
