﻿using System;
using org.goodspace.Data.Radio.Adif.Types;
using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents an ADIF.NET tag where the underlying value is numeric as represented by 
    /// a value of type nullable <see cref="int"/>.
    /// </summary>
    public class IntegerTag : Tag<int?>, ITag
    {

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public virtual int MinValue => int.MinValue;

        /// <summary>
        /// Maximum numeric value.
        /// </summary>
        public virtual int MaxValue => int.MaxValue;

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType ADIFType => new AdifInteger();

        /// <summary>
        /// Creates a new instance of the <see cref="IntegerTag"/> class.
        /// </summary>
        public IntegerTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="IntegerTag"/> class.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public IntegerTag(int value) : base(value) { }

        /// <summary>
        /// Creates a new instance of the <see cref="IntegerTag"/> class.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public IntegerTag(double value)
        {
            if (!value.IsWholeNumber())
                throw new ArgumentException($"Invalid {nameof(AdifInteger)}: {value}");

            SetValue(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override object? ConvertValue(object? value)
        {
            if (value is int || value is int? || (value != null && value.GetType().IsAssignableFrom(typeof(int?))))
                return (int?)value;
            else if (value is double dblVal && dblVal.IsWholeNumber())
                return Convert.ToInt32(dblVal);
            else
            {
                try
                {
                    return AdifInteger.Parse(value == null ? string.Empty : value.ToString());
                }
                catch (Exception ex)
                {
                    throw new ValueConversionException(value, Name, ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool ValidateValue(object? value)
        {
            if (value is null)
                return true;

            if (value is string strVal && string.IsNullOrEmpty(strVal))
                return true;

            try
            {
                var val = ConvertValue(value);

                if (val is int intVal)
                {
                    if (intVal < MinValue)
                        return false;
                    else if (MaxValue > MinValue && intVal > MaxValue)
                        return false;

                    return true;
                }
                else if (val is int?)
                    return true;
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}