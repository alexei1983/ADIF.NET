using System;
using System.Collections.Generic;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class SponsoredAwardListTag : MultiValueStringTag, ITag {

    public override string ValueSeparator => Values.COMMA.ToString();

    public override IADIFType ADIFType => new ADIFSponsoredAwardList();

    public string[] Prefixes => Values.SponsoredAwardPrefixes.GetValues();

    public SponsoredAwardListTag() { }

    public SponsoredAwardListTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
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
    /// 
    /// </summary>
    /// <param name="award"></param>
    public override void AddValue(string award)
    {
      if (string.IsNullOrEmpty(award))
        throw new ArgumentException("Award is required.", nameof(award));

      if (!ADIFSponsoredAwardList.TryParse(award, out _))
        throw new ArgumentException($"Award '{award}' does not have a valid sponsored prefix.");

      base.AddValue(award);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="award"></param>
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
          exceptions.Add(new Exception($"Award '{award}' does not have a valid sponsored prefix."));
      }

      if (exceptions.Count > 1)
        throw new AggregateException(exceptions.ToArray());
      else if (exceptions.Count == 1)
        throw exceptions[0];
    }
  }
}
