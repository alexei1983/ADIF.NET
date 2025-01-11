using System.Globalization;
using System.Xml;
using org.goodspace.Data.Radio.Adif.Helpers;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents an application-defined ADIF field and value.
    /// </summary>
    public class AppDefTag : Tag<object>, ITag, ICloneable, IFormattable
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => $"{AdifTags.AppDef}{ProgramId ?? Values.DEFAULT_PROGRAM_ID}_{FieldName ?? string.Empty}";

        /// <summary>
        /// Value of the tag as a <see cref="string"/>.
        /// </summary>
        public override string TextValue => AppUserDefHelper.GetTextValueByType(DataType, Value);

        /// <summary>
        /// Field name.
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// Program ID.
        /// </summary>
        public string ProgramId
        {

            get
            {
                return programId ?? Values.DEFAULT_PROGRAM_ID;
            }

            set
            {
                AppUserDefHelper.ValidateProgramId(value);
                programId = value;
            }
        }

        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public new string DataType { get; set; } = string.Empty;

        /// <summary>
        /// Whether or not the tag is application-defined.
        /// </summary>
        public override bool IsAppDef => true;

        /// <summary>
        /// Creates a new application-defined field.
        /// </summary>
        public AppDefTag() { }

        /// <summary>
        /// Creates a new application-defined field.
        /// </summary>
        /// <param name="fieldName">Application-defined field name.</param>
        /// <param name="programId">Short name/ID of the program that defines the field (cannot contain an underscore).</param>
        /// <param name="dataType">ADIF data type indicator.</param>
        /// <param name="value">Field value.</param>
        public AppDefTag(string fieldName, string programId, string dataType, object? value)
        {
            FieldName = fieldName;
            ProgramId = programId;
            DataType = dataType ?? string.Empty;

            if (value != null)
                SetValue(value);
        }

        /// <summary>
        /// Creates a new application-defined field.
        /// </summary>
        /// <param name="fieldName">Application-defined field name.</param>
        /// <param name="programId">Short name/ID of the program that defines the field (cannot contain an underscore).</param>
        /// <param name="dataType">ADIF data type indicator.</param>
        public AppDefTag(string fieldName, string programId, string dataType) : this(fieldName, programId, dataType, null) { }

        /// <summary>
        /// Creates a new application-defined field.
        /// </summary>
        /// <param name="fullFieldName">Full application-defined field name, including the APP prefix, program ID, and field name.</param>
        /// <param name="dataType">ADIF data type indicator.</param>
        /// <param name="value">Field value.</param>
        public AppDefTag(string fullFieldName, string dataType, object? value)
        {
            var fieldParts = AppUserDefHelper.SplitAppDefinedFieldName(fullFieldName);

            ProgramId = fieldParts[1];
            FieldName = fieldParts[2];
            DataType = dataType;

            if (value != null)
                SetValue(value);
        }

        /// <summary>
        /// Creates a new application-defined field.
        /// </summary>
        /// <param name="fullFieldName">Full application-defined field name, including the APP prefix, program ID, and field name.</param>
        /// <param name="value">Field value.</param>
        public AppDefTag(string fullFieldName, object value) : this(fullFieldName, string.Empty, value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Value to set.</param>
        public new void SetValue(object? value)
        {
            var convertedVal = ConvertValue(value);
            base.SetValue(convertedVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Value to convert.</param>
        public override object? ConvertValue(object? value)
        {
            return value is not null ? AppUserDefHelper.ConvertValueByType(value, DataType) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        public override string ToString(string? format, IFormatProvider? provider)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";

            provider ??= CultureInfo.CurrentCulture;

            switch (format)
            {
                case "A":
                case "a":
                    var retVal = string.Empty;

                    if (!string.IsNullOrEmpty(FieldName))
                    {
                        retVal = $"{Values.TAG_OPENING}{("a".Equals(format) ? ToString("n", provider) : ToString("N", provider))}";
                        retVal = $"{retVal}{Values.VALUE_LENGTH_CHAR}{ValueLength}{(!string.IsNullOrEmpty(DataType) ? $"{Values.COLON}{DataType.ToUpperInvariant()}" : string.Empty)}{Values.TAG_CLOSING}";
                        retVal = $"{retVal}{TextValue} ";
                    }
                    return retVal;

                case "F":
                    return FieldName ?? string.Empty;

                case "P":
                    return ProgramId ?? Values.DEFAULT_PROGRAM_ID;

                default:
                    return base.ToString(format, provider);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        public override XmlElement? ToXml(XmlDocument document)
        {
            if (document == null)
                return null;

            var el = document.CreateElement(ADXValues.ADX_APP_ELEMENT);
            el.InnerText = TextValue;
            el.SetAttribute(ADXValues.ADX_PROGRAMID_ATTRIBUTE, ProgramId);
            el.SetAttribute(ADXValues.ADX_FIELDNAME_ATTRIBUTE, FieldName);

            if (!string.IsNullOrEmpty(DataType))
                el.SetAttribute(ADXValues.ADX_TYPE_ATTRIBUTE, DataType);

            return el;
        }

        string? programId;
    }
}
