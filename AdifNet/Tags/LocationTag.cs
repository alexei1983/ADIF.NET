
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// 
    /// </summary>
    public class LocationTag : StringTag, ITag
    {
        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifLocation();

        /// <summary>
        /// Value of the tag as a <see cref="string"/>.
        /// </summary>
        public override string TextValue
        {
            get
            {
                return location != null ? location.ToString() : string.Empty;
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
                SetValue(location);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        public LocationTag(Location location)
        {
            SetValue(location);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetValue(object? value)
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
            SetValue((object?)value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override object? ConvertValue(object? value)
        {
            if (value is Location locationObj)
                return locationObj;
            else
            {
                var valStr = value is string strLoc ? strLoc : value != null ? value.ToString() : string.Empty;

                if (!string.IsNullOrEmpty(valStr))
                    return AdifType.Parse(valStr);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool ValidateValue(object? value)
        {
            if (value is Location)
                return true;

            return AdifType.TryParse(value is null ? string.Empty : value.ToString(), out _);
        }

        /// <summary>
        /// 
        /// </summary>
        public Location? GetLocation()
        {
            return location;
        }

        Location? location;
    }
}
