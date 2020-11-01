using System;

namespace ADIF.NET.Attributes {

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
  public class LegacyValueAttribute : Attribute {

    public bool Legacy { get; set; }

    public long ValidStart { get; set; }

    public long ValidEnd { get; set; }

    public DateTime? Start {

      get {

        if (ValidStart > 0) {

          var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(ValidStart);

          return dateTimeOffset.DateTime;
          }
        return null;
        }
      }

    public DateTime? End {

      get {

        if (ValidEnd > 0) {

          var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(ValidEnd);

          return dateTimeOffset.DateTime;
          }
        return null;
        }
      }

    public LegacyValueAttribute(bool legacy, long validStart, long validEnd) {
      Legacy = legacy;
      ValidStart = validStart;
      ValidEnd = validEnd;
      }

    public LegacyValueAttribute(bool legacy) : this(legacy, 0, 0) {
      }

    public LegacyValueAttribute() : this(true, 0, 0) { }
    }
  }
