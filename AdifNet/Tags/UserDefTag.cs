﻿using System.Globalization;
using System.Xml;
using org.goodspace.Data.Radio.Adif.Helpers;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the specification of a user-defined QSO field.
    /// </summary>
    public class UserDefTag : StringTag, ITag, IFormattable
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.UserDef;

        /// <summary>
        /// Value of the tag as a <see cref="string"/>.
        /// </summary>
        public override string TextValue
        {
            get
            {
                var value = $"{FieldName}";

                if (CustomOptions != null && CustomOptions.Length > 0)
                    value = $"{value}{AdifConstants.Comma}{AdifConstants.CurlyBraceOpen}{string.Join(AdifConstants.Comma.ToString(), CustomOptions)}{AdifConstants.CurlyBraceClose}";
                else if (UpperBound > LowerBound)
                    value = $"{value}{AdifConstants.Comma}{AdifConstants.CurlyBraceOpen}{LowerBound}{AdifConstants.Colon}{UpperBound}{AdifConstants.CurlyBraceClose}";

                return value;
            }
        }

        /// <summary>
        /// Name of the user-defined field.
        /// </summary>
        public string FieldName
        {
            get { return fieldName ?? string.Empty; }

            set
            {
                if (!string.IsNullOrEmpty(value))
                    AppUserDefHelper.ValidateUserDefFieldName(value);

                fieldName = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Whether or not the tag is a header tag.
        /// </summary>
        public override bool Header => true;

        /// <summary>
        /// Whether or not the tag is a user-defined tag.
        /// </summary>
        public override bool IsUserDef => true;

        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public new string DataType { get; set; }

        /// <summary>
        /// Numeric ID of the user-defined field.
        /// </summary>
        public int FieldId { get; set; }

        /// <summary>
        /// User-defined enumeration values.
        /// </summary>
        public string[] CustomOptions { get; set; } = [];

        /// <summary>
        /// Minimum valid numeric value.
        /// </summary>
        public double LowerBound { get; set; }

        /// <summary>
        /// Maximum valid numeric value.
        /// </summary>
        public double UpperBound { get; set; }

        /// <summary>
        /// Creates a new USERDEF tag.
        /// </summary>
        public UserDefTag() : this(string.Empty, 0, string.Empty) { }

        /// <summary>
        /// Creates a new USERDEF tag.
        /// </summary>
        /// <param name="fieldName">Name of the user-defined field.</param>
        /// <param name="fieldId">Numeric ID of the user-defined field.</param>
        /// <param name="dataType">ADIF data type indicator.</param>
        public UserDefTag(string fieldName, int fieldId, string dataType)
        {
            FieldName = fieldName;
            FieldId = fieldId;
            DataType = dataType;
        }

        /// <summary>
        /// Creates a new USERDEF tag.
        /// </summary>
        /// <param name="fieldName">Name of the user-defined field.</param>
        /// <param name="fieldId">Numeric ID of the user-defined field.</param>
        /// <param name="options">User-defined enumeration values.</param>
        public UserDefTag(string fieldName, int fieldId, params string[] options) : this(fieldName, fieldId, DataTypes.Enumeration)
        {
            CustomOptions = options;
        }

        /// <summary>
        /// Creates a new USERDEF tag.
        /// </summary>
        /// <param name="fieldName">Name of the user-defined field.</param>
        /// <param name="fieldId">Numeric ID of the user-defined field.</param>
        /// <param name="lowerBound">Minimum valid numeric value.</param>
        /// <param name="upperBound">Maximum valid numeric value.</param>
        public UserDefTag(string fieldName, int fieldId, double lowerBound, double upperBound) : this(fieldName, fieldId, DataTypes.Number)
        {
            if (upperBound < lowerBound)
                throw new ArgumentException("Upper bound numeric value cannot be less than lower bound numeric value.");

            UpperBound = upperBound;
            LowerBound = lowerBound;
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

                    if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(FieldName) && FieldId > 0)
                    {
                        retVal = $"{AdifConstants.TagOpen}{("a".Equals(format) ? ToString("n", provider) : ToString("N", provider))}{ToString("I", provider)}";
                        retVal = $"{retVal}{AdifConstants.ValueLengthIndicator}{ValueLength}{(!string.IsNullOrEmpty(DataType) ? $"{AdifConstants.Colon}{DataType.ToUpperInvariant()}" : string.Empty)}{AdifConstants.TagClose}";
                        retVal = $"{retVal}{TextValue} ";
                    }
                    return retVal;

                case "I":
                    return FieldId > 0 ? FieldId.ToString() : string.Empty;

                case "F":
                    return FieldName;

                default:
                    return base.ToString(format, provider);
            }
        }

        /// <summary>
        /// Retrieves the <see cref="XmlElement"/> representation of the current tag.
        /// <paramref name="document">XML document object.</paramref>
        /// </summary>
        public override XmlElement? ToXml(XmlDocument document)
        {
            if (document == null)
                return null;

            var el = document.CreateElement(Name);
            el.InnerText = FieldName;
            el.SetAttribute(AdxConstants.AttributeFieldId, FieldId.ToString());

            if (!string.IsNullOrEmpty(DataType))
                el.SetAttribute(AdxConstants.AttributeType, DataType);

            if (CustomOptions != null)
            {
                var enumStr = string.Empty;
                for (var x = 0; x < CustomOptions.Length; x++)
                {
                    enumStr += CustomOptions[x];
                    if ((x + 1) < CustomOptions.Length)
                        enumStr += AdifConstants.Comma.ToString();
                }
                el.SetAttribute(AdxConstants.AttributeEnum,
                                $"{AdifConstants.CurlyBraceOpen}{enumStr}{AdifConstants.CurlyBraceClose}");
            }

            if (LowerBound < UpperBound)
                el.SetAttribute(AdxConstants.AttributeRange,
                                $"{AdifConstants.CurlyBraceOpen}{LowerBound}{AdifConstants.Colon}{UpperBound}{AdifConstants.CurlyBraceClose}");

            return el;
        }

        string? fieldName;
    }
}
