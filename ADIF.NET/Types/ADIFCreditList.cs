using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the CreditList ADIF type.
  /// </summary>
  public class ADIFCreditList : ADIFString, IADIFType {

    public override string Type => DataTypes.CreditList;

    public override bool MultiValue => true;

    public static CreditList Parse(string s)
    {
      return ParseCreditList(s);
    }

    public static bool TryParse(string s, out CreditList result)
    {
      try
      {
        result = ParseCreditList(s);
        return true;
      }
      catch
      {
        result = null;
        return false;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="o"></param>
    public static bool IsValidValue(object o)
    {
      if (o is null) 
        return false;

       return IsValidValue(o.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static bool IsValidValue(string s)
    {
      if (string.IsNullOrEmpty(s))
        return false;

      try
      {
        ParseCreditList(s);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    static CreditList ParseCreditList(string s)
    {
      if (s == null)
        return null;

      // example string: IOTA,WAS:LOTW&CARD,DXCC:CARD
      var list = new CreditList();

      // split by comma first
      var split = s.Split(Values.COMMA);

      foreach (var val in split)
      {
        // if a colon is present, split again
        if (val.Contains(Values.COLON))
        {
          var creditQslSplit = val.Split(Values.COLON);

          if (creditQslSplit == null || creditQslSplit.Length != 2)
            throw new Exception("Error in CreditList value.");

          // now try to split by ampersand
          if (creditQslSplit[1].Contains(Values.AMPERSAND))
          {
            var mediumSplit = creditQslSplit[1].Split(Values.AMPERSAND);

            if (mediumSplit == null)
              mediumSplit = new string[] { };

            foreach (var medium in mediumSplit)
            {
              if (!Values.QSLMediums.IsValid(medium))
                throw new Exception($"QSL medium '{medium}' is not valid for credit '{creditQslSplit[0]}'.");

              list.Add(creditQslSplit[0], medium);
            }
          } else
          {
            // if no ampersand is present, validate against QSL medium
            if (!Values.QSLMediums.IsValid(creditQslSplit[1]))
              throw new Exception($"QSL medium '{creditQslSplit[1]}' is not valid for credit '{creditQslSplit[0]}'.");

            list.Add(creditQslSplit[0], creditQslSplit[1]);
          } // end ampersand check
        } else
        {
          // if a colon was not present, simply validate against the credit enumeration
          list.Add(val);
        }
      }

      return list;
    }
  }

  /// <summary>
  /// Represents an ADIF CreditList value.
  /// </summary>
  public class CreditList : List<CreditList.CreditListMember>, IFormattable {

    public void Add(string credit, string medium)
    {
      this.Add(new CreditListMember(credit, medium));
    }

    public void Add(string credit)
    {
      this.Add(new CreditListMember(credit));
    }

    public IEnumerable<string> GetMediums(string credit)
    {
      return this.Where(c => !string.IsNullOrEmpty(c.Medium) && 
                             c.Credit.Equals(credit, StringComparison.OrdinalIgnoreCase))?
                 .Select(c => c.Medium);
    }

    public struct CreditListMember {
      public string Credit { get; }
      public string Medium { get; }

      public CreditListMember(string credit) : this(credit, null)
      {
      }

      public CreditListMember(string credit, string medium)
      {
        Medium = medium;
        Credit = credit;
      }
    }

    public override string ToString()
    {
      return this.ToString("G", CultureInfo.CurrentCulture);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      if (string.IsNullOrEmpty(format))
        format = "G";

      if (provider == null)
        provider = CultureInfo.CurrentCulture;

      switch (format.ToUpper())
      {
        case "G":
        case "A":

          var handled = new List<string>();
          var result = string.Empty;

          for (var x = 0; x < this.Count; x++)
          {
            if (handled.Contains(this[x].Credit.ToUpper()))
              continue;

            if (x > 0 && (x + 1) < this.Count)
              result += Values.COMMA.ToString();

            result += this[x].Credit;

            var mediums = GetMediums(this[x].Credit)?.ToArray();

            if (mediums != null && mediums.Length > 0)
            {
              result += Values.COLON.ToString();

              for (var y = 0; y < mediums.Length; y++)
              {
                result += mediums[y];
                if ((y + 1) < mediums.Length)
                  result += Values.AMPERSAND.ToString();
              }
            }

              handled.Add(this[x].Credit.ToUpper());

          }

          return result;

        default:
          throw new FormatException("Invalid format string.");
      }
    }
  }
}
