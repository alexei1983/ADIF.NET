﻿using System;
using System.Collections.Generic;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class MultiValueStringTag : StringTag, ITag {

    public virtual int MaxValueCount { get; }

    public virtual int MinValueCount { get; }

    public virtual int ValueCount => values?.Count ?? 0;

    public virtual bool AllowValueCountOverMax { get; }

    public override string TextValue
    {
      get
      {
        if (values.Count > 0)
        {
          var retVal = string.Empty;
          for (var x = 0; x < values.Count; x++)
          {
            retVal += $"{values[x]}";
            if ((x + 1) < values.Count)
              retVal += ValueSeparator;
          }

          return retVal;
        }

        return base.TextValue;
      }
    }

    public MultiValueStringTag()
    {
      if (values == null)
        values = new List<string>();
    }

    public MultiValueStringTag(string value) 
    {
      if (values == null)
        values = new List<string>();

      SetValue(value);
    }

    public virtual void AddValue(string value)
    {
      if (!string.IsNullOrEmpty(value))
      {
        if ((MaxValueCount > 0 && values.Count < MaxValueCount) || AllowValueCountOverMax || MaxValueCount <= 0)
          values.Add(value);
      }
    }

    public override void ClearValue()
    {
      values.Clear();
      base.ClearValue();
    }

    public virtual string GetValue(int index)
    {
       if (index < 1)
        throw new IndexOutOfRangeException("Index must be greater than zero.");

       return values.Count < index ? values[index] : throw new IndexOutOfRangeException("Invalid index.");
    }

    public virtual IEnumerable<string> GetValues()
    {
      foreach (var value in values)
        yield return value;
    }

    public override void SetValue(string value)
    {
      if (values == null)
        values = new List<string>();

      if (ValidateValue(value))
      {
        base.SetValue(value);
        var splitVals = SplitValue(value, !AllowValueCountOverMax);
        if (splitVals != null)
          values.AddRange(splitVals);
      }
    }

    public override bool ValidateValue(object value)
    {
      if (base.ValidateValue(value))
      {
        try
        {
          SplitValue(value.ToString(), true);
          return true;
        }
        catch
        {
          return false;
        }
      }

      return false;
    }

    string[] SplitValue(string value, bool throwExceptionOnInvalidCount = true)
    {
      if (!string.IsNullOrEmpty(value))
      {
        if (value.Contains(ValueSeparator))
        {
          var splitVals = value.Split(new string[] { ValueSeparator }, StringSplitOptions.RemoveEmptyEntries);
          if (splitVals != null)
          {
            if (splitVals.Length > MaxValueCount && MaxValueCount > 0 && throwExceptionOnInvalidCount)
              throw new Exception($"{GetValueCountExceptionText(splitVals.Length, true)}");
            else if (splitVals.Length < MinValueCount && MinValueCount > 0 && throwExceptionOnInvalidCount)
              throw new Exception($"{GetValueCountExceptionText(splitVals.Length, false)}");

            //values.AddRange(splitVals);
            return splitVals;
          }
        }
        else
        {
          //values.Add(value);
          return new string[] { value };
        }
      }

      return null;
    }

    string GetValueCountExceptionText(int foundValueCount, bool minMax)
    {
       return minMax ? $"{Name} tag must not contain more than {MaxValueCount} {(MaxValueCount == 1 ? "value" : "values")}, " + 
                       $"found {foundValueCount} {(foundValueCount == 1 ? "value" : "values")}" :
                       $"{Name} tag must contain at least {MinValueCount} {(MinValueCount == 1 ? "value" : "values")}, " +
                       $"found {foundValueCount} {(foundValueCount == 1 ? "value" : "values")}";
    }

    List<string> values;
  }
}