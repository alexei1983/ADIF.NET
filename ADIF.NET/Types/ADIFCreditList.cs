using System;
using System.Collections;
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
    /// ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.CreditList;

    /// <summary>
    /// ADIF data type name.
    /// </summary>
    public override string TypeName => DataTypeNames.CreditList;

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
    /// Parses the specified string as an ADIF CreditList object.
    /// </summary>
    /// <param name="s">String to parse.</param>
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
  public class CreditList : IFormattable, IEnumerable<CreditList.CreditListMember>, IEnumerable {
 
    List<CreditListMember> internalList;

    /// <summary>
    /// Total number of credit/medium combinations in the CreditList object.
    /// </summary>
    public int Count => internalList.Count;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i">Zero-based index of the credit/medium combination to retrieve.</param>
    public CreditListMember this[int i]
    {
      get
      {
        return internalList[i];
      }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="CreditList"/> class.
    /// </summary>
    public CreditList()
    {
      internalList = new List<CreditListMember>();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="CreditList"/> class.
    /// </summary>
    /// <param name="adifCreditListString">ADIF string representation of the CreditList object.</param>
    public CreditList(string adifCreditListString) : this()
    {
      if (!string.IsNullOrEmpty(adifCreditListString))
      {
        if (ADIFCreditList.TryParse(adifCreditListString, out CreditList creditList))
        {
          foreach (var member in creditList)
            internalList.Add(member);
        }
      }

    }

    /// <summary>
    /// Adds a new credit/medium combination to the current <see cref="CreditList"/> instance.
    /// </summary>
    /// <param name="credit">Credit enumeration member.</param>
    /// <param name="medium">QSL medium enumeration member.</param>
    public void Add(string credit, string medium)
    {
      if (string.IsNullOrEmpty(credit))
        throw new ArgumentException("Credit is required.", nameof(credit));

      if (string.IsNullOrEmpty(medium))
        throw new ArgumentException("QSL medium is required.", nameof(medium));

      if (!HasMediumInCredit(credit, medium))
        internalList.Add(new CreditListMember(credit, medium));

      var member = GetCreditWithNoMedium(credit);

      if (!string.IsNullOrEmpty(member.Credit))
        internalList.Remove(member);
    }

    /// <summary>
    /// Adds a new credit to the current <see cref="CreditList"/> instance.
    /// </summary>
    /// <param name="credit">Credit enumeration member.</param>
    public void Add(string credit)
    {
      if (string.IsNullOrEmpty(credit))
        throw new ArgumentException("Credit is required.", nameof(credit));

      if (!HasCredit(credit))
        internalList.Add(new CreditListMember(credit));
    }

    /// <summary>
    /// Retrieves all QSL mediums for the specified credit.
    /// </summary>
    /// <param name="credit">Credit enumeration member for which QSL mediums will be retrieved.</param>
    public IEnumerable<string> GetMediums(string credit)
    {
      if (string.IsNullOrEmpty(credit))
        throw new ArgumentException("Credit is required.", nameof(credit));

      return internalList.Where(c => !string.IsNullOrEmpty(c.Medium) && 
                               (c.Credit ?? string.Empty).Equals(credit, StringComparison.OrdinalIgnoreCase))?
                         .Select(c => c.Medium);
    }

    /// <summary>
    /// Retrieves the distinct list of credits in the current <see cref="CreditList"/> instance.
    /// </summary>
    public IEnumerable<string> GetCredits()
    {
      return internalList.Where(c => !string.IsNullOrEmpty(c.Credit))?
                         .Select(c => c.Credit)?
                         .Distinct();
    }

    /// <summary>
    /// Determines whether or not the current <see cref="CreditList"/> instance contains the specified credit.
    /// </summary>
    /// <param name="credit">Credit enumeration member to search.</param>
    public bool HasCredit(string credit)
    {
      if (string.IsNullOrEmpty(credit))
        throw new ArgumentException("Credit is required.", nameof(credit));

      return !string.IsNullOrEmpty(internalList.FirstOrDefault(c => (c.Credit ?? string.Empty).Equals(credit, StringComparison.OrdinalIgnoreCase))
                                                                    .Credit);
    }

    /// <summary>
    /// Determines whether or not the current <see cref="CreditList"/> instance contains the specified credit and medium combination.
    /// </summary>
    /// <param name="credit">Credit enumeration member to search.</param>
    /// <param name="medium">QSL medium enumeration member to search.</param>
    public bool HasMediumInCredit(string credit, string medium)
    {
      if (string.IsNullOrEmpty(credit))
        throw new ArgumentException("Credit is required.", nameof(credit));

      if (string.IsNullOrEmpty(medium))
        throw new ArgumentException("QSL medium is required.", nameof(medium));

      return !string.IsNullOrEmpty(internalList.FirstOrDefault(c => (c.Credit ?? string.Empty).Equals(credit, StringComparison.OrdinalIgnoreCase) &&
                                                                    (c.Medium ?? string.Empty).Equals(medium, StringComparison.OrdinalIgnoreCase))
                                                                    .Medium);
    }

    /// <summary>
    /// Removes the specified credit and QSL medium combination from the current <see cref="CreditList"/> instance.
    /// </summary>
    /// <param name="credit">Credit enumeration member to remove.</param>
    /// <param name="medium">QSL medium enumeration member to remove.</param>
    public bool RemoveMedium(string credit, string medium)
    {
      if (string.IsNullOrEmpty(credit))
        throw new ArgumentException("Credit is required.", nameof(credit));

      if (string.IsNullOrEmpty(medium))
        throw new ArgumentException("QSL medium is required.", nameof(medium));

      var member = internalList.FirstOrDefault(m => (m.Credit ?? string.Empty).Equals(credit, StringComparison.OrdinalIgnoreCase) &&
                                                    (m.Medium ?? string.Empty).Equals(medium, StringComparison.OrdinalIgnoreCase));

      if (!string.IsNullOrEmpty(member.Credit) && !string.IsNullOrEmpty(member.Medium))
        return internalList.Remove(member);

      return false;
    }

    /// <summary>
    /// Removes the specified credit (and all its QSL mediums, if any) from the current <see cref="CreditList"/> instance.
    /// </summary>
    /// <param name="credit">Credit enumeration member to remove.</param>
    public bool RemoveCredit(string credit)
    {
      if (string.IsNullOrEmpty(credit))
        throw new ArgumentException("Credit is required.", nameof(credit));

      var members = internalList.Where(m => (m.Credit ?? string.Empty).Equals(credit, StringComparison.OrdinalIgnoreCase)).ToList();

      if (members != null && members.Count > 0)
      {
        foreach (var member in members)
        {
          if (!internalList.Remove(member))
            return false;
        }
      }
      return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="credit"></param>
    CreditListMember GetCreditWithNoMedium(string credit)
    {
      return internalList.FirstOrDefault(c => !string.IsNullOrEmpty(c.Credit) && 
                                              string.IsNullOrEmpty(c.Medium) &&
                                              c.Credit.Equals(credit, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="CreditList"/>.
    /// </summary>
    public IEnumerator<CreditListMember> GetEnumerator()
    {
      return internalList.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="CreditList"/>.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return internalList.GetEnumerator();
    }

    /// <summary>
    /// Represents a single credit and QSL medium combination.
    /// </summary>
    public struct CreditListMember : IEquatable<CreditListMember> {

      /// <summary>
      /// Credit enumeration member.
      /// </summary>
      public string Credit { get; }

      /// <summary>
      /// QSL medium enumeration member.
      /// </summary>
      public string Medium { get; }

      /// <summary>
      /// Creates a new <see cref="CreditListMember"/> instance.
      /// </summary>
      /// <param name="credit">Credit enumeration member.</param>
      public CreditListMember(string credit) : this(credit, null)
      {
      }

      /// <summary>
      /// Creates a new <see cref="CreditListMember"/> instance.
      /// </summary>
      /// <param name="credit">Credit enumeration member.</param>
      /// <param name="medium">QSL medium enumeration member.</param>
      public CreditListMember(string credit, string medium)
      {
        Medium = medium;
        Credit = credit;
      }

      /// <summary>
      /// Calculates the object's hash code.
      /// </summary>
      public override int GetHashCode()
      {
        unchecked
        {
          const int hashingBase = (int)2166136261;
          const int hashingMultiplier = 16924616;

          var hash = hashingBase;
          hash = (hash * hashingMultiplier) ^ (!string.IsNullOrEmpty(Credit) ? Credit.ToUpperInvariant().GetHashCode() : 0);
          hash = (hash * hashingMultiplier) ^ (GetType().GetHashCode());
          hash = (hash * hashingMultiplier) ^ (!string.IsNullOrEmpty(Medium) ? Medium.ToUpperInvariant().GetHashCode() : 0);

          return hash;
        }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      public override bool Equals(object obj)
      {
        if (obj is null)
          return false;

        if (obj is CreditListMember member)
          return member.Equals(this);

        return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="member"></param>
      /// <returns></returns>
      public bool Equals(CreditListMember member)
      {
        if (!(member.Credit ?? string.Empty).Equals(this.Credit ?? string.Empty, StringComparison.OrdinalIgnoreCase))
          return false;

        return (member.Medium ?? string.Empty).Equals(this.Medium ?? string.Empty, StringComparison.OrdinalIgnoreCase);
      }
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="CreditList"/> instance.
    /// </summary>
    public override string ToString()
    {
      return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="CreditList"/> instance.
    /// </summary>
    /// <param name="format">Format string.</param>
    /// <param name="provider">Format provider.</param>
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
          var total = 0;

          for (var x = 0; x < Count; x++)
          {
            if (handled.Contains(this[x].Credit.ToUpper()))
              continue;

            if (x > 0 && total < Count)
              result += Values.COMMA.ToString();

            result += this[x].Credit;

            var mediums = GetMediums(this[x].Credit)?.ToArray();

            if (mediums != null && mediums.Length > 0)
            {
              result += Values.COLON.ToString();

              for (var y = 0; y < mediums.Length; y++)
              {
                result += mediums[y];
                total++;
                if ((y + 1) < mediums.Length)
                  result += Values.AMPERSAND.ToString();
              }
            }
            else
              total++;

              handled.Add(this[x].Credit.ToUpper());
          }

          return result;

        default:
          throw new FormatException("Invalid format string.");
      }
    }
  }
}
