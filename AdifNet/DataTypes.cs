using org.goodspace.Data.Radio.Adif.Attributes;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// ADIF data type abbreviations.
    /// </summary>
    [Enumeration]
    internal static class DataTypes
    {
        /// <summary>
        /// ADIF AwardList data type.
        /// </summary>
        public const string AwardList = "A";

        /// <summary>
        /// ADIF CreditList data type.
        /// </summary>
        public const string CreditList = "C";

        /// <summary>
        /// ADIF SponsoredAwardList data type.
        /// </summary>
        public const string SponsoredAwardList = "P";

        /// <summary>
        /// ADIF Boolean type.
        /// </summary>
        public const string Boolean = "B";

        /// <summary>
        /// ADIF Digit type.
        /// </summary>
        public const string Digit = "N";

        /// <summary>
        /// ADIF Number type.
        /// </summary>
        public const string Number = "N";

        /// <summary>
        /// ADIF Character type.
        /// </summary>
        public const string Character = "S";

        /// <summary>
        /// ADIF IntlCharacter type.
        /// </summary>
        public const string IntlCharacter = "I";

        /// <summary>
        /// ADIF Date type.
        /// </summary>
        public const string Date = "D";

        /// <summary>
        /// ADIF Time type.
        /// </summary>
        public const string Time = "T";

        /// <summary>
        /// ADIF String type.
        /// </summary>
        public const string String = "S";

        /// <summary>
        /// ADIF IntlString type.
        /// </summary>
        public const string IntlString = "I";

        /// <summary>
        /// ADIF MultilineString type.
        /// </summary>
        public const string MultilineString = "M";

        /// <summary>
        /// ADIF IntlMultilineString type.
        /// </summary>
        public const string IntlMultilineString = "G";

        /// <summary>
        /// ADIF Enumeration type.
        /// </summary>
        public const string Enumeration = "E";

        /// <summary>
        /// ADIF Location type.
        /// </summary>
        public const string Location = "L";
    }
}
