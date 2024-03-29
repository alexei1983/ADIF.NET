﻿using System;
using System.Globalization;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// An ADIF.NET tag whose value represents a time in the format HHMM or HHMMSS.
  /// </summary>
  public class TimeTag : DateTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string TextValue
    {
      get
      {
        if (Value != DateTime.MinValue)
          return Value.ToString(Value.Second > 0 ? Values.ADIF_TIME_FORMAT_LONG : Values.ADIF_TIME_FORMAT_SHORT,
                                FormatProvider ?? CultureInfo.CurrentCulture);
        
        return string.Empty;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public override string FormatString { get; set; } = Values.ADIF_TIME_FORMAT_SHORT;

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFTime();

    /// <summary>
    /// 
    /// </summary>
    public bool ConvertToUTC { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="TimeTag"/> class.
    /// </summary>
    public TimeTag() { }

    /// <summary>
    /// Creates a new instance of the <see cref="TimeTag"/> class.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public TimeTag(DateTime value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value) {

      if (!(value is null)) {

        var final = DateTime.MinValue;

        if (value is DateTime dateTime)
          final = dateTime;
        else {
          var objStr = value.ToString();

          var formatString = objStr.Length > 4 ? Values.ADIF_TIME_FORMAT_LONG : Values.ADIF_TIME_FORMAT_SHORT;

          if (DateTime.TryParseExact(objStr,
                                     formatString,
                                     FormatProvider,
                                     DateTimeStyles.AllowInnerWhite | 
                                     DateTimeStyles.AllowLeadingWhite | 
                                     DateTimeStyles.AllowTrailingWhite | 
                                     DateTimeStyles.NoCurrentDateDefault,
                                     out DateTime dateTimeParsed))
            final = dateTimeParsed;
          }

        if (final != DateTime.MinValue && ConvertToUTC)
          final = final.ToUniversalTime();

        return final;
        }

      return null;
      }
    }
  }
