using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the CreditList ADIF type.
  /// </summary>
  public class ADIFCreditList : ADIFType<string>, IADIFType {

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.CreditList;

    /// <summary>
    /// Whether or not the type is multivalued.
    /// </summary>
    public override bool MultiValue => true;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static CreditList Parse(string s)
    {
      return ParseCreditList(s);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
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
            throw new CreditListException("Invalid value.", val);

          // validate the credit part of the string
          if (!Values.Credits.IsValid(creditQslSplit[0]))
            throw new CreditListException($"Credit '{creditQslSplit[0]}' is not valid.", creditQslSplit[0]);

          // now try to split by ampersand
          if (creditQslSplit[1].Contains(Values.AMPERSAND))
          {
            var mediumSplit = creditQslSplit[1].Split(Values.AMPERSAND);

            if (mediumSplit == null)
              mediumSplit = new string[] { };

            foreach (var medium in mediumSplit)
            {
              if (!Values.QSLMediums.IsValid(medium))
                throw new CreditListException($"QSL medium '{medium}' is not valid for credit '{creditQslSplit[0]}'.", $"{creditQslSplit[0]}:{medium}");

              list.Add(creditQslSplit[0], medium);
            }
          } else
          {
            // if no ampersand is present, validate against QSL medium
            if (!Values.QSLMediums.IsValid(creditQslSplit[1]))
              throw new CreditListException($"QSL medium '{creditQslSplit[1]}' is not valid for credit '{creditQslSplit[0]}'.", $"{creditQslSplit[0]}:{creditQslSplit[1]}");

            list.Add(creditQslSplit[0], creditQslSplit[1]);
          } // end ampersand check
        } else
        {
          // if a colon was not present, simply validate against the credit enumeration
          if (!Values.Credits.IsValid(val))
            throw new CreditListException($"Credit '{val}' is not valid.", val);

          list.Add(val);
        }
      }

      return list;
    }
  }

  /// <summary>
  /// Represents an ADIF CreditList value.
  /// </summary>
  public class CreditList : List<CreditList.CreditListMember>, 
                            IList<CreditList.CreditListMember>,
                            IFormattable {
 
    public void Add(string credit, string medium)
    {
      if (string.IsNullOrEmpty(credit) || string.IsNullOrEmpty(medium))
        throw new CreditListException("Credit and medium are required.");

      Add(new CreditListMember(credit, medium));
    }

    public void Add(string credit)
    {
      Add(new CreditListMember(credit));
    }

    public IEnumerable<string> GetMediums(string credit)
    {
      return this.Where(c => !string.IsNullOrEmpty(c.Medium) && 
                             c.Credit.Equals(credit, StringComparison.OrdinalIgnoreCase))?
                 .Select(c => c.Medium);
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<string> GetCredits()
    {
      return this.Where(c => !string.IsNullOrEmpty(c.Credit))?
                 .Select(c => c.Credit)?
                 .Distinct();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="credit"></param>
    public bool HasCredit(string credit)
    {
      return !string.IsNullOrEmpty(this.FirstOrDefault(c => c.Credit.Equals(credit, StringComparison.OrdinalIgnoreCase))
                                                             .Credit);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="credit"></param>
    /// <param name="medium"></param>
    public bool HasMediumInCredit(string credit, string medium)
    {
      return !string.IsNullOrEmpty(this.FirstOrDefault(c => c.Credit.Equals(credit, StringComparison.OrdinalIgnoreCase) &&
                                                            c.Medium.Equals(medium, StringComparison.OrdinalIgnoreCase))
                                                             .Medium);
    }

    /// <summary>
    /// 
    /// </summary>
    public struct CreditListMember {

      /// <summary>
      /// 
      /// </summary>
      public string Credit { get; }

      /// <summary>
      /// 
      /// </summary>
      public string Medium { get; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="credit"></param>
      public CreditListMember(string credit) : this(credit, null)
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="credit"></param>
      /// <param name="medium"></param>
      public CreditListMember(string credit, string medium)
      {
        Medium = medium;
        Credit = credit;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public override string ToString()
    {
      return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
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

          for (var x = 0; x < Count; x++)
          {
            if (handled.Contains(this[x].Credit.ToUpper()))
              continue;

            if (x >= 0 && (x + 1) < Count)
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
