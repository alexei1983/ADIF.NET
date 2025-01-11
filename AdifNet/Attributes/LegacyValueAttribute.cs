﻿
namespace org.goodspace.Data.Radio.Adif.Attributes
{

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public class LegacyValueAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Legacy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ValidStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ValidEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Start
        {

            get
            {

                if (ValidStart > 0)
                {

                    var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(ValidStart);

                    return dateTimeOffset.DateTime;
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? End
        {

            get
            {

                if (ValidEnd > 0)
                {

                    var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(ValidEnd);

                    return dateTimeOffset.DateTime;
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="legacy"></param>
        /// <param name="validStart"></param>
        /// <param name="validEnd"></param>
        public LegacyValueAttribute(bool legacy, long validStart, long validEnd)
        {
            Legacy = legacy;
            ValidStart = validStart;
            ValidEnd = validEnd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="legacy"></param>
        public LegacyValueAttribute(bool legacy) : this(legacy, 0, 0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public LegacyValueAttribute() : this(true, 0, 0) { }
    }
}