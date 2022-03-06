using System;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class LocationTag : StringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override IADIFType ADIFType => new ADIFLocation();

    /// <summary>
    /// 
    /// </summary>
    public override string TextValue
    {
      get
      {
        return location != null ? location.ToString() : base.TextValue;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public LocationTag() : base() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public LocationTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="decimalDegrees"></param>
    /// <param name="type"></param>
    public LocationTag(decimal decimalDegrees, LocationType type)
    {
      var location = new Location(decimalDegrees, type);
      if (location != null)
        SetValue(location.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override void SetValue(object value)
    {
      if (ConvertValue(value) is Location locationObj)
      {
        location = locationObj;
        base.SetValue(locationObj.ToString());
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override void SetValue(string value)
    {
      SetValue((object)value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      if (value is Location locationObj)
        return locationObj;
      else
      {
        var valStr = value is string ? (string)value : value != null ? value.ToString() : string.Empty;

        if (!string.IsNullOrEmpty(valStr))
          return ADIFLocation.Parse(valStr);
      }

      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    public Location GetLocation()
    {
      return location;
    }

    Location location;
  }
}
