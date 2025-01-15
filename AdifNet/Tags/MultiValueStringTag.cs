
using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// 
    /// </summary>
    public class MultiValueStringTag : StringTag, ITag
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int MaxValueCount { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int MinValueCount { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int ValueCount => values?.Count ?? 0;

        /// <summary>
        /// 
        /// </summary>
        public virtual bool AllowValueCountOverMax { get; }

        /// <summary>
        /// 
        /// </summary>
        public override string TextValue
        {
            get
            {
                if (values.Count > 0)
                {
                    var retVal = string.Empty;
                    for (var x = 0; x < values.Count; x++)
                    {
                        retVal += $"{values[x]}";
                        if ((x + 1) < values.Count)
                            retVal += ValueSeparator;
                    }

                    return retVal;
                }

                return base.TextValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MultiValueStringTag()
        {
            values ??= [];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public MultiValueStringTag(string value)
        {
            values ??= [];

            SetValue(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public MultiValueStringTag(params string[] values)
        {
            this.values ??= [];

            if (values != null)
            {
                foreach (var value in values)
                    AddValue(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public virtual void AddValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Contains(ValueSeparator))
                    throw new MultiValueStringException("Value cannot contain the delimiter character.", value);

                if ((MaxValueCount > 0 && values.Count < MaxValueCount) || AllowValueCountOverMax || MaxValueCount <= 0)
                    values.Add(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClearValue()
        {
            values.Clear();
            base.ClearValue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public virtual string GetValue(int index)
        {
            if (index < 1)
                throw new IndexOutOfRangeException("Index must be greater than zero.");

            return values.Count < index ? values[index] : throw new IndexOutOfRangeException("Invalid index.");
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IEnumerable<string> GetValues()
        {
            foreach (var value in values)
                yield return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override object? ConvertValue(object? value)
        {
            values ??= [];
            var strVal = value is string v ? v : value != null ? value.ToString() : string.Empty;

            try
            {
                return SplitValue(strVal ?? string.Empty, !AllowValueCountOverMax);
            }
            catch (Exception ex)
            {
                throw new ValueConversionException(value, Name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetValue(string value)
        {
            if (ConvertValue(value) is string[] values)
            {
                this.values.AddRange(values);
                base.SetValue(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool ValidateValue(object? value)
        {
            if (base.ValidateValue(value))
            {
                try
                {
                    SplitValue(value?.ToString(), true);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="throwExceptionOnInvalidCount"></param>
        string[] SplitValue(string? value, bool throwExceptionOnInvalidCount = true)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Contains(ValueSeparator))
                {
                    var splitValues = value.Split(new string[] { ValueSeparator }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitValues != null)
                    {
                        if (splitValues.Length > MaxValueCount && MaxValueCount > 0 && throwExceptionOnInvalidCount)
                            throw new MultiValueStringException($"{GetValueCountExceptionText(splitValues.Length, true)}", value);
                        else if (splitValues.Length < MinValueCount && MinValueCount > 0 && throwExceptionOnInvalidCount)
                            throw new MultiValueStringException($"{GetValueCountExceptionText(splitValues.Length, false)}", value);

                        return splitValues;
                    }
                }
                else
                {
                    return [value];
                }
            }

            return [];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foundValueCount"></param>
        /// <param name="minMax"></param>
        string GetValueCountExceptionText(int foundValueCount, bool minMax)
        {
            return minMax ? $"{Name} tag must not contain more than {MaxValueCount} {(MaxValueCount == 1 ? "value" : "values")}, " +
                            $"found {foundValueCount} {(foundValueCount == 1 ? "value" : "values")}" :
                            $"{Name} tag must contain at least {MinValueCount} {(MinValueCount == 1 ? "value" : "values")}, " +
                            $"found {foundValueCount} {(foundValueCount == 1 ? "value" : "values")}";
        }

        List<string> values;
    }
}
