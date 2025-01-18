using System.Xml;
using org.goodspace.Data.Radio.Adif.Helpers;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents a user-defined QSO field and its value.
    /// </summary>
    public class UserDefValueTag : Tag<object>, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => field.FieldName ?? string.Empty;

        /// <summary>
        /// Numeric ID of the user-defined field.
        /// </summary>
        public int FieldId => field.FieldId;

        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string DataType => field.DataType ?? string.Empty;

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => AppUserDefHelper.GetAdifType(field.DataType) ?? AdifType<object>.Default;

        /// <summary>
        /// 
        /// </summary>
        public override string? FormatString
        {
            get
            {
                return DataType == DataTypes.Date ? AdifConstants.DateFormat :
                  DataType == DataTypes.Time ? AdifConstants.TimeFormatLong : null;
            }
        }

        /// <summary>
        /// Whether or not the tag is a user-defined tag.
        /// </summary>
        public override bool IsUserDef => true;

        /// <summary>
        /// Value of the tag as a <see cref="string"/>.
        /// </summary>
        public override string TextValue => AppUserDefHelper.GetTextValueByType(DataType, Value);

        /// <summary>
        /// Whether or not the tag value is restricted to the list of enumeration values.
        /// </summary>
        public override bool RestrictOptions => field.CustomOptions?.Length > 0;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumeration.FromUserDefinedTag(field) ?? [];

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public double MinValue => field.LowerBound;

        /// <summary>
        /// Maximum numeric value.
        /// </summary>
        public double MaxValue => field.UpperBound;

        /// <summary>
        /// Creates a new user-defined QSO tag.
        /// </summary>
        /// <param name="field"><see cref="UserDefTag"/> representing the definition of the user-defined QSO tag.</param>
        /// <param name="value">Initial tag value.</param>
        public UserDefValueTag(UserDefTag field, object value)
        {
            this.field = field ?? throw new ArgumentNullException(nameof(field), "User-defined tag definition is required.");
            if (value != null)
                SetValue(value);
        }

        /// <summary>
        /// Creates a new user-defined QSO tag.
        /// </summary>
        /// <param name="field"><see cref="UserDefTag"/> representing the definition of the user-defined QSO tag.</param>
        public UserDefValueTag(UserDefTag field)
        {
            this.field = field ?? throw new ArgumentNullException(nameof(field), "User-defined tag definition is required.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override object? ConvertValue(object? value)
        {
            return value is not null ? AppUserDefHelper.ConvertValueByType(value, DataType) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public new void SetValue(object? value)
        {
            var convertedVal = ConvertValue(value);
            base.SetValue(convertedVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool ValidateValue(object? value)
        {
            if (base.ValidateValue(value))
            {
                object? convObj;
                try
                {
                    convObj = ConvertValue(value);

                    if (convObj is string convStrVal && string.IsNullOrEmpty(convStrVal))
                        return true;
                }
                catch
                {
                    return false;
                }

                if (Options?.Count > 0 && RestrictOptions && convObj is string strVal)
                {
                    return Options.IsValid(strVal);
                }
                else if (MaxValue > MinValue && (convObj is double || convObj is double?))
                {
                    var dblVal = (double?)convObj;
                    return (dblVal.HasValue && dblVal >= MinValue && dblVal <= MaxValue) || !dblVal.HasValue;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public override XmlElement? ToXml(XmlDocument document)
        {
            if (document == null)
                return null;

            var el = document.CreateElement(AdifTags.UserDef);
            el.InnerText = TextValue;
            el.SetAttribute(AdxConstants.AttributeFieldName, Name);

            return el;
        }

        readonly UserDefTag field;
    }
}
