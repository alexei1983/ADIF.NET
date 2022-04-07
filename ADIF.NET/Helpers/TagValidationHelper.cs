using System;
using System.Collections.Generic;
using System.Linq;
using ADIF.NET.Tags;
using ADIF.NET.Types;

namespace ADIF.NET.Helpers {

  /// <summary>
  /// Helper class for validating tags in a header or QSO.
  /// </summary>
  public static class TagValidationHelper {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="expectedType"></param>
    public static void ValidateExpectedValueType(ITag tag, Type expectedType)
    {
      if (tag == null || expectedType == null)
        throw new Exception("Cannot validate expected value type: missing required parameters.");

      if (tag.ExpectedValueType != expectedType)
        throw new Exception($"Value for tag '{tag.Name}' is not of the expected type.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frequencyTag"></param>
    /// <param name="bandTag"></param>
    public static void ValidateFrequencyBand(ITag frequencyTag, ITag bandTag)
    {
      if (frequencyTag == null || bandTag == null)
        return;

      ValidateExpectedValueType(frequencyTag, typeof(double?));
      ValidateExpectedValueType(bandTag, typeof(string));

      var freq = frequencyTag.Value as double?;
      var band = bandTag.Value as string;

      if (!string.IsNullOrEmpty(band) && freq.HasValue)
      {
        if (!Band.IsFrequencyInBand(band, freq.Value))
          throw new Exception($"Frequency {freq.Value} is not in the {band} band.");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dxccTag"></param>
    /// <param name="primarySubDivTag"></param>
    public static void ValidatePrimaryAdminSubdivision(ITag dxccTag, ITag primarySubDivTag)
    {
      if (dxccTag == null || primarySubDivTag == null)
        return;

      ValidateExpectedValueType(dxccTag, typeof(string));
      ValidateExpectedValueType(primarySubDivTag, typeof(string));

      var dxcc = DXCCHelper.ConvertDXCC(dxccTag.TextValue);

      if (!DXCCHelper.ValidatePrimarySubdivision(dxcc, primarySubDivTag.TextValue))
        throw new Exception($"Primary administrative subdivision '{primarySubDivTag.TextValue}' does not belong to DXCC entity {dxccTag.TextValue}.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dxccTag"></param>
    /// <param name="primarySubDivTag"></param>
    /// <param name="secondarySubDivTag"></param>
    public static void ValidateAdminSubdivisions(ITag dxccTag, ITag primarySubDivTag, ITag secondarySubDivTag)
    {
      if (dxccTag == null || secondarySubDivTag == null || primarySubDivTag == null)
        return;

      ValidateExpectedValueType(dxccTag, typeof(string));
      ValidateExpectedValueType(secondarySubDivTag, typeof(string));
      ValidateExpectedValueType(primarySubDivTag, typeof(string));

      var dxcc = DXCCHelper.ConvertDXCC(dxccTag.TextValue);

      if (!DXCCHelper.ValidateSecondarySubdivision(dxcc, primarySubDivTag.TextValue, secondarySubDivTag.TextValue))
        throw new Exception($"Secondary administrative subdivision '{secondarySubDivTag.TextValue}' does not belong to primary administrative " + 
                            $"subdivision '{primarySubDivTag.TextValue}' in DXCC entity {dxccTag.TextValue}.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="latTag"></param>
    /// <param name="longTag"></param>
    public static void ValidateLatLong(ITag latTag, ITag longTag)
    {
      if (latTag == null && longTag == null)
        return;
      else if (latTag == null || longTag == null)
        throw new Exception("Latitude and longitude are both required for validation.");

      if (!(latTag.Value is Location latVal) || !(longTag.Value is Location longVal))
        throw new Exception("Invalid latitude/longitude values.");

      if (latVal.LocationType != LocationType.Latitude)
        throw new Exception($"Latitude value '{latTag.TextValue}' is invalid.");

      if (longVal.LocationType != LocationType.Longitude)
        throw new Exception($"Longitude value '{longTag.TextValue}' is invalid.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="list"></param>
    /// <param name="stringComparison"></param>
    public static void ValidateValueInList(ITag tag, 
                                           ICollection<string> list,
                                           StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
    {
      if (tag == null || list == null || list.Count < 1)   
        return;

      var value = tag.TextValue ?? string.Empty;

      if (string.IsNullOrEmpty(list.FirstOrDefault(l => value.Equals(l, stringComparison))))
        throw new Exception($"Value '{value}' is not valid for tag '{tag.Name}'.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="adifVersion"></param>
    public static void ValidateTagVersion(ITag tag, Version adifVersion)
    {
      if (tag == null || adifVersion == null)
        return;
      
      var tagName = tag.Name;
      
      if (tagName.StartsWith(ADIFTags.AppDef, StringComparison.OrdinalIgnoreCase))
        tagName = ADIFTags.AppDef;
      else if (tag.IsUserDef)
        tagName = ADIFTags.UserDef;

      var result = SQLiteHelper.Instance.ExecuteScalar<long>(TAG_VERSION_SQL,
                                                             new Dictionary<string, object>() { { "@TagName", tagName },
                                                                                                { "@Version", adifVersion.ToString()} });

      if (result != 1)
        throw new Exception($"Tag '{tag.Name}' is not valid for ADIF version {adifVersion}.");
    }

    /// <summary>
    /// Validates the specified RST components.
    /// </summary>
    /// <param name="readability">The readability value (1-5)</param>
    /// <param name="strength">The strength value (1-9)</param>
    /// <param name="tone">The tone value (1-9)</param>
    /// <param name="suffix">The suffix (A, C, K, M, S, X)</param>
    public static void ValidateRst(int readability, int strength, int tone, string suffix)
    {
      if (readability > 5 || readability < 1)
        throw new ArgumentException($"Invalid RST readability value: {readability}");

      if (strength > 9 || strength < 1)
        throw new ArgumentException($"Invalid RST strength value: {strength}");

      if (tone > 0)
      {
        if (tone > 9)
          throw new ArgumentException($"Invalid RST tone value: {tone}");
      }

      if (!string.IsNullOrEmpty(suffix))
      {
        suffix = suffix.ToUpper();
        if (suffix != "A" && suffix != "C" && suffix != "K" && suffix != "M" && suffix != "S" && suffix != "X")
          throw new ArgumentException($"Invalid RST suffix: {suffix}");
      }
    }

    const string TAG_VERSION_SQL = "SELECT 1 FROM Tags WHERE Name = @TagName AND MinVersion <= @Version AND (MaxVersion IS NULL OR MaxVersion >= @Version)";
  }
}
