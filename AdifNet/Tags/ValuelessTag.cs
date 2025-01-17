﻿using System.Xml;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents an ADIF tag that stores no value.
    /// </summary>
    public class ValuelessTag : StringTag, ITag
    {
        /// <summary>
        /// Tag value.
        /// </summary>
        public override string Value => string.Empty;

        /// <summary>
        /// Value of the tag as a <see cref="string"/>.
        /// </summary>
        public override string TextValue => string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public override bool SuppressLength => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetValue(object? value)
        {
            throw new NotImplementedException($"{Name} tag does not allow a value.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetValue(string value)
        {
            throw new NotImplementedException($"{Name} tag does not allow a value.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override object? ConvertValue(object? value)
        {
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool ValidateValue(object? value)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        public override XmlElement? ToXml(XmlDocument document)
        {
            return null;
        }
    }
}
