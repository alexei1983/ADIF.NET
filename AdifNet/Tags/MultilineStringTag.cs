﻿using System;
using org.goodspace.Data.Radio.Adif.Types;
using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents an ADIF.NET tag where the underlying value is of type <see cref="string"/>.
    /// </summary>
    public class MultilineStringTag : Tag<string>, ITag
    {

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType ADIFType => new AdifMultilineString();

        /// <summary>
        /// Converts the specified object to an ADIF String.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        public override object? ConvertValue(object? value)
        {
            try
            {
                var result = AdifMultilineString.Parse(value == null ? string.Empty : value.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw new ValueConversionException(value, Name, ex);
            }
        }

        /// <summary>
        /// Determines whether or not the specified object can be represented as an ADIF String.
        /// </summary>
        /// <param name="value">Value to validate.</param>
        public override bool ValidateValue(object? value)
        {
            try
            {
                ConvertValue(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MultilineStringTag"/> class.
        /// </summary>
        public MultilineStringTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="MultilineStringTag"/> class.
        /// </summary>
        /// <param name="value">Initial value for the tag.</param>
        public MultilineStringTag(string value) : base(value) { }

    }
}
