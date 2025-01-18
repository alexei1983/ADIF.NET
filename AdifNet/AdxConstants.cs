
namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// XML element and attribute names for parsing files in ADX format.
    /// </summary>
    internal static class AdxConstants
    {
        /// <summary>
        /// The root ADX element.
        /// </summary>
        public const string ElementRoot = "ADX";

        /// <summary>
        /// The RECORDS element in ADX.
        /// </summary>
        public const string ElementRecords = "RECORDS";

        /// <summary>
        /// The RECORD element in ADX.
        /// </summary>
        public const string ElementRecord = "RECORD";

        /// <summary>
        /// The HEADER element in ADX.
        /// </summary>
        public const string ElementHeader = "HEADER";

        /// <summary>
        /// The ENUM attribute in ADX.
        /// </summary>
        public const string AttributeEnum = "ENUM";

        /// <summary>
        /// The RANGE attribute in ADX.
        /// </summary>
        public const string AttributeRange = "RANGE";

        /// <summary>
        /// The FIELDID attribute in ADX.
        /// </summary>
        public const string AttributeFieldId = "FIELDID";

        /// <summary>
        /// The TYPE attribute in ADX.
        /// </summary>
        public const string AttributeType = "TYPE";

        /// <summary>
        /// The PROGRAMID attribute in ADX.
        /// </summary>
        public const string AttributeProgramId = "PROGRAMID";

        /// <summary>
        /// The APP element in ADX.
        /// </summary>
        public const string ElementApp = "APP";

        /// <summary>
        /// The FIELDNAME attribute in ADX.
        /// </summary>
        public const string AttributeFieldName = "FIELDNAME";
    }
}
