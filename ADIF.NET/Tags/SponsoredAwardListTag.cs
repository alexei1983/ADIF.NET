using System;
using System.Collections.Generic;
using ADIF.NET.Types;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF tag of type SponsoredAwardList.
  /// </summary>
  public class SponsoredAwardListTag : MultiValueStringTag, ITag {

    /// <summary>
    /// String that delimits values in a multivalued ADIF tag.
    /// </summary>
    public override string ValueSeparator => Values.COMMA.ToString();

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFSponsoredAwardList();

    /// <summary>
    /// Valid sponsored award prefixes.
    /// </summary>
    public string[] Prefixes => Values.SponsoredAwardPrefixes.GetValues();

    /// <summary>
    /// Creates a new instance of the <see cref="SponsoredAwardListTag"/>.
    /// </summary>
    public SponsoredAwardListTag() { }

    /// <summary>
    /// Creates a new instance of the <see cref="SponsoredAwardListTag"/>.
    /// </summary>
    /// <param name="value"></param>
    public SponsoredAwardListTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new instance of the <see cref="SponsoredAwardListTag"/>.
    /// </summary>
    /// <param name="values"></param>
    public SponsoredAwardListTag(params string[] values) : base(values) { }

    /// <summary>
    /// Determines whether or not the specified object is a valid value for the current ADIF tag.
    /// </summary>
    /// <param name="value">Object to validate.</param>
    public override bool ValidateValue(object value)
    {
      if (value != null)
      {
        try
        {
          ADIFSponsoredAwardList.Parse(value.ToString());
          return true;
        }
        catch
        {
          return false;
        }
      }

      return false;
    }

    /// <summary>
    /// Converts the specified object to the appropriate type for the current ADIF tag.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    public override object ConvertValue(object value)
    {
      var strVal = string.Empty;
      if (value is string)
        strVal = (string)value;
      else
        strVal = value != null ? value.ToString() : string.Empty;

      try
      {
        return ADIFSponsoredAwardList.Parse(strVal);
      }
      catch (Exception ex)
      {
        throw new ValueConversionException(value, Name, ex);
      }

    }

    /// <summary>
    /// Adds a new sponsored award value to the tag.
    /// </summary>
    /// <param name="award">Sponsored award to add.</param>
    public override void AddValue(string award)
    {
      if (string.IsNullOrEmpty(award))
        throw new ArgumentException("Award is required.", nameof(award));

      if (!ADIFSponsoredAwardList.TryParse(award, out _))
        throw new SponsoredAwardListException($"Award '{award}' does not have a valid sponsored prefix.", award);

      base.AddValue(award);
    }

    /// <summary>
    /// Validates the specified sponsored awards.
    /// </summary>
    /// <param name="awards">Array of awards to validate.</param>
    void ValidateAwards(params string[] awards)
    {
      if (awards == null || awards.Length == 0)
        return;

      var prefixes = Prefixes;
      var exceptions = new List<Exception>();

      foreach (var award in awards)
      {
        var checkedCount = 0;

        foreach (var prefix in prefixes)
        {
          if (!award.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            checkedCount++;
        }

        if (checkedCount == Prefixes.Length)
          exceptions.Add(new SponsoredAwardListException($"Award '{award}' does not have a valid sponsored prefix."));
      }

      if (exceptions.Count > 1)
        throw new AggregateException(exceptions.ToArray());
      else if (exceptions.Count == 1)
        throw exceptions[0];
    }
  }
}
