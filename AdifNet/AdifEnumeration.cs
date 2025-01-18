using org.goodspace.Data.Radio.Adif.Helpers;
using org.goodspace.Data.Radio.Adif.Tags;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Represents an ADIF enumeration.
    /// </summary>
    public class AdifEnumeration : List<AdifEnumerationValue>
    {
        /// <summary>
        /// The enumeration type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifEnumeration"/> class.
        /// </summary>
        public AdifEnumeration() : this(string.Empty, []) { }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifEnumeration"/> class.
        /// </summary>
        /// <param name="type">The enumeration type.</param>
        public AdifEnumeration(string type) : this(type, []) { }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifEnumeration"/> class.
        /// </summary>
        /// <param name="type">The enumeration type.</param>
        /// <param name="values">Values to add to the current enumeration.</param>
        public AdifEnumeration(string type, params AdifEnumerationValue[] values)
        {
            Type = type;
            if (values != null)
            {
                foreach (var v in values)
                {
                    if (v != null)
                        Add(v);
                }
            }
        }

        /// <summary>
        /// Creates an ADIF enumeration using the custom options in a user-defined tag.
        /// </summary>
        /// <param name="tag">User-defined tag from which to derive the enumeration.</param>
        public static AdifEnumeration? FromUserDefinedTag(UserDefTag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag), "User-defined tag is required.");

            if (tag.CustomOptions != null)
            {
                var enumVal = new AdifEnumeration(tag.FieldName);
                foreach (var option in tag.CustomOptions)
                    enumVal.Add(new AdifEnumerationValue(option));

                return enumVal;
            }

            return null;
        }

        /// <summary>
        /// Retrieves an ADIF enumeration by type.
        /// </summary>
        /// <param name="type">The enumeration type to retrieve.</param>
        public static AdifEnumeration? Get(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("Enumeration type is required.", nameof(type));

            var enumeration = new AdifEnumeration(type);

            var query = string.Empty;
            Dictionary<string, object?> parameters = [];

            if (type == Resources.EnumNameDxcc)
                query = Resources.SqlRetrieveDxccEnum;
            else if (type == Resources.EnumNameBand)
                query = Resources.SqlRetrieveBandsEnum;
            else if (type == Resources.EnumNameCredit)
                query = Resources.SqlRetrieveCreditEnum;
            else if (type == Resources.EnumNameDarcDok)
                query = Resources.SqlRetrieveDarcDokEnum;
            else if (type == nameof(AdifBoolean))
                return new AdifEnumeration(nameof(AdifBoolean), new AdifEnumerationValue(AdifConstants.BooleanTrue, AdifConstants.BooleanTrueDisplay),
                                                                new AdifEnumerationValue(AdifConstants.BooleanFalse, AdifConstants.BooleanFalseDisplay));
            else if (type == Resources.EnumNamePrimarySubdivision)
            {
                query = Resources.SqlRetrievePrimarySubdivisionEnum;
                parameters.Add(Resources.SqlParameterParentType, type);
            }
            else if (type == Resources.EnumNameSecondarySubdivision)
            {
                query = Resources.SqlRetrieveSecondarySubdivisionEnum;
                parameters.Add(Resources.SqlParameterParentType, type);
            }
            else
            {
                query = Resources.SqlRetrieveEnum;
                parameters.Add(Resources.SqlParameterEnumType, type);
            }

            var data = SQLiteHelper.Instance.ReadData(query, parameters);

            foreach (dynamic d in data)
            {
                var enumVal = new AdifEnumerationValue(d);
                if (!string.IsNullOrEmpty(enumVal.Code))
                    enumeration.Add(enumVal);
            }

            return enumeration.Count > 0 ? enumeration : null;
        }

        /// <summary>
        /// Determines whether or not the specified value is valid for the current <see cref="AdifEnumeration"/>.
        /// </summary>
        /// <param name="value">Value to check for validity.</param>
        public bool IsValid(string? value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value is required.");

            return GetValues().Any(v => value.Equals(v, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Retrieves a string array of codes for the current <see cref="AdifEnumeration"/>.
        /// </summary>
        public string[] GetValues()
        {
            return this.Select(v => v.Code).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public AdifEnumerationValue? GetValue(string? code)
        {
            code ??= string.Empty;
            return this.FirstOrDefault(e => code.Equals(e.Code, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Retrieves the enumeration values belonging to the specified parent.
        /// </summary>
        /// <param name="parentType">The enumeration type of the parent.</param>
        /// <param name="parentCode">The code of the parent enumeration value.</param>
        public static IEnumerable<AdifEnumerationValue> GetChildren(string parentType, string parentCode)
        {
            if (string.IsNullOrEmpty(parentType))
                throw new ArgumentException("Parent type is required.", nameof(parentType));

            if (string.IsNullOrEmpty(parentCode))
                throw new ArgumentException("Parent code is required.", nameof(parentCode));

            var query = Resources.SqlRetrieveChildEnum;

            if (Resources.EnumNameDxcc.Equals(parentType))
                query = Resources.SqlRetrieveChildDxccEnum;

            var data = SQLiteHelper.Instance.ReadData(query,
                                                      new Dictionary<string, object?>() { { Resources.SqlParameterParentType, parentType },
                                                                                          { Resources.SqlParameterParent, parentCode } });

            foreach (dynamic d in data)
            {
                var enumVal = new AdifEnumerationValue(d);
                if (!string.IsNullOrEmpty(enumVal.Code))
                    yield return enumVal;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static IEnumerable<AdifEnumerationValue> GetPrimarySubdivisions(string countryCode)
        {
            return GetChildren(Resources.EnumNameDxcc, countryCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="primarySubdivisionCode"></param>
        public static IEnumerable<AdifEnumerationValue> GetSecondarySubdivisions(string countryCode, string primarySubdivisionCode)
        {
            if (string.IsNullOrEmpty(countryCode))
                throw new ArgumentException("DXCC is required.", nameof(countryCode));

            if (string.IsNullOrEmpty(primarySubdivisionCode))
                throw new ArgumentException("Primary subdivision code is required.", nameof(primarySubdivisionCode));

            var data = SQLiteHelper.Instance.ReadData(Resources.SqlRetrieveChildPrimarySubdivisionEnum,
                                                      new Dictionary<string, object?>() { { Resources.SqlParameterParentType, Resources.EnumNamePrimarySubdivision },
                                                                                          { Resources.SqlParameterParent, primarySubdivisionCode },
                                                                                          { Resources.SqlParameterCountryCode, countryCode } }) ;

            foreach (dynamic d in data)
            {
                var enumVal = new AdifEnumerationValue(d);
                if (!string.IsNullOrEmpty(enumVal.Code))
                    yield return enumVal;
            }
        }
    }
}
