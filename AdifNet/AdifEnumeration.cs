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

            if (type == DXCC_ENUM_STRING)
                query = RETRIEVE_DXCC_SQL;
            else if (type == BAND_ENUM_STRING)
                query = RETRIEVE_BANDS_SQL;
            else if (type == CREDIT_ENUM_STRING)
                query = RETRIEVE_CREDIT_SQL;
            else if (type == DARC_DOK_ENUM_STRING)
                query = RETRIEVE_DARC_DOK_SQL;
            else if (type == nameof(AdifBoolean))
                return new AdifEnumeration(nameof(AdifBoolean), new AdifEnumerationValue(Values.ADIF_BOOLEAN_TRUE, Values.ADIF_BOOLEAN_TRUE_DISPLAY),
                                                                new AdifEnumerationValue(Values.ADIF_BOOLEAN_FALSE, Values.ADIF_BOOLEAN_FALSE_DISPLAY));
            else if (type == PRIMARY_SUB_ENUM_STRING)
                query = RETRIEVE_PRIMARY_SUB_SQL;
            else if (type == SECONDARY_SUB_ENUM_STRING)
                query = RETRIEVE_SECONDARY_SUB_SQL;
            else
                query = ENUM_RETRIEVE_SQL.Replace("{{TYPE}}", type.Replace("'", "''"));

            var data = SQLiteHelper.Instance.ReadData(query);

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
        public IEnumerable<AdifEnumerationValue> GetChildren(string parentType, string parentCode)
        {
            if (string.IsNullOrEmpty(parentType))
                throw new ArgumentException("Parent type is required.", nameof(parentType));

            if (string.IsNullOrEmpty(parentCode))
                throw new ArgumentException("Parent code is required.", nameof(parentCode));

            var query = ENUM_RETRIEVE_CHILDREN_SQL;

            if (DXCC_ENUM_STRING.Equals(parentType))
                query = RETRIEVE_DXCC_CHILDREN_SQL;

            var data = SQLiteHelper.Instance.ReadData(query,
                                                      new Dictionary<string, object?>() { { "@ParentType", parentType },
                                                                                    { "@Parent", parentCode } });

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
        /// <param name="primarySubdivisionCode"></param>
        public IEnumerable<AdifEnumerationValue> GetSecondarySubdivisions(string countryCode, string primarySubdivisionCode)
        {
            if (string.IsNullOrEmpty(countryCode))
                throw new ArgumentException("DXCC is required.", nameof(countryCode));

            if (string.IsNullOrEmpty(primarySubdivisionCode))
                throw new ArgumentException("Primary subdivision code is required.", nameof(primarySubdivisionCode));

            var data = SQLiteHelper.Instance.ReadData(RETRIEVE_PRIMARY_SUB_CHILDREN_SQL,
                                                      new Dictionary<string, object?>() { { "@ParentType", PRIMARY_SUB_ENUM_STRING },
                                                                                    { "@Parent", primarySubdivisionCode },
                                                                                    { "@CountryCode", countryCode } });

            foreach (dynamic d in data)
            {
                var enumVal = new AdifEnumerationValue(d);
                if (!string.IsNullOrEmpty(enumVal.Code))
                    yield return enumVal;
            }
        }

        const string ENUM_RETRIEVE_SQL = "SELECT Code, DisplayName, ImportOnly, Legacy, Parent, ParentType FROM \"Enumerations\" WHERE Type = '{{TYPE}}' ORDER BY DisplayName, Code";
        const string RETRIEVE_DXCC_SQL = "SELECT Code, Name AS DisplayName, Deleted AS ImportOnly, Deleted AS Legacy FROM \"CountryCodes\" ORDER BY Name, Code";
        const string RETRIEVE_BANDS_SQL = "SELECT Name AS Code, Name AS DisplayName, 0 AS Legacy, 0 AS ImportOnly FROM \"Bands\"";
        const string RETRIEVE_CREDIT_SQL = "SELECT CreditFor AS Code, Sponsor || ' - ' || Award AS DisplayName, 0 AS Legacy, 0 AS ImportOnly FROM \"Credits\" ORDER BY CreditFor";
        const string RETRIEVE_DARC_DOK_SQL = "SELECT Code, District || ' - ' || Dok AS DisplayName, 0 AS Legacy, 0 AS ImportOnly FROM \"DarcDok\" ORDER BY Code";
        const string ENUM_RETRIEVE_CHILDREN_SQL = "SELECT Code, DisplayName, ImportOnly, Legacy, Parent, ParentType FROM \"Enumerations\" WHERE ParentType = @ParentType AND Parent = @Parent ORDER BY DisplayName, Code";
        const string RETRIEVE_DXCC_CHILDREN_SQL = "SELECT Code, Name AS DisplayName, Deprecated AS ImportOnly, Deprecated AS Legacy, CAST(CountryCode AS TEXT) AS Parent, '" + DXCC_ENUM_STRING + "' AS ParentType FROM \"PrimaryAdminSubdivisions\" WHERE CAST(CountryCode AS TEXT) = @Parent AND '" + DXCC_ENUM_STRING + "' = @ParentType";
        const string RETRIEVE_PRIMARY_SUB_CHILDREN_SQL = "SELECT Code, Name AS DisplayName, Deleted AS ImportOnly, Deleted AS Legacy, PrimarySubdivisionCode AS Parent, '" + PRIMARY_SUB_ENUM_STRING + "' AS ParentType FROM \"SecondaryAdminSubdivisions\" WHERE PrimarySubdivisionCode = @Parent AND '" + PRIMARY_SUB_ENUM_STRING + "' = @ParentType AND CAST(CountryCode AS TEXT) = @CountryCode";
        const string RETRIEVE_PRIMARY_SUB_SQL = "SELECT Code, Name AS DisplayName, Deprecated AS ImportOnly, Deprecated AS Legacy, CAST(CountryCode AS TEXT) AS Parent, '" + DXCC_ENUM_STRING + "' AS ParentType FROM \"PrimaryAdminSubdivisions\" ORDER BY CountryCode, Name, Code";
        const string RETRIEVE_SECONDARY_SUB_SQL = "SELECT s.Code, s.Name AS DisplayName, s.Deleted AS ImportOnly, s.Deleted AS Legacy, s.PrimarySubdivisionCode AS Parent, '" + PRIMARY_SUB_ENUM_STRING + "' AS ParentType FROM \"SecondaryAdminSubdivisions\" s ORDER BY s.CountryCode, s.PrimarySubdivisionCode, s.Name, s.Code";
        const string CREDIT_ENUM_STRING = "Credit";
        const string BAND_ENUM_STRING = "Band";
        const string DXCC_ENUM_STRING = "DXCC";
        const string DARC_DOK_ENUM_STRING = "DARCDOK";
        const string PRIMARY_SUB_ENUM_STRING = "PrimarySubdivision";
        const string SECONDARY_SUB_ENUM_STRING = "SecondarySubdivision";
    }
}
