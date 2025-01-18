
namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// ADIF data type names.
    /// </summary>
    internal class DataTypeNames
    {
        /// <summary>
        /// A comma-delimited list of members of the ADIF Award enumeration.
        /// </summary>
        public const string AwardList = "AwardList";

        /// <summary>
        /// A comma-delimited list where each list item is either: a member of the ADIF Credit enumeration or a member of the 
        /// ADIF Credit enumeration followed by a colon and an ampersand-delimited list of members of the ADIF QSL_Medium enumeration.
        /// </summary>
        public const string CreditList = "CreditList";

        /// <summary>
        /// A comma-delimited list of members of the ADIF Sponsored_Award enumeration.
        /// </summary>
        public const string SponsoredAwardList = "SponsoredAwardList";

        /// <summary>
        /// An ADIF boolean value where true is represented by 'Y' or 'y' and false is 
        /// represented by 'N' or 'n'.
        /// </summary>
        public const string Boolean = "Boolean";

        /// <summary>
        /// An ASCII character whose code lies in the range of 48 through 57, inclusive.
        /// </summary>
        public const string Digit = "Digit";

        /// <summary>
        /// A sequence of one or more Digits representing a decimal number, optionally preceded by a minus sign and optionally 
        /// including a single decimal point.
        /// </summary>
        public const string Number = "Number";

        /// <summary>
        /// An ASCII character whose code lies in the range of 32 through 126, inclusive.
        /// </summary>
        public const string Character = "Character";

        /// <summary>
        /// A Unicode character (encoded with UTF-8) excluding line break CR (code 13) and LF (code 10) characters.
        /// </summary>
        public const string IntlCharacter = "IntlCharacter";

        /// <summary>
        /// ADIF Date type consisting of 8 Digits representing a UTC date in YYYYMMDD format.
        /// </summary>
        public const string Date = "Date";

        /// <summary>
        /// ADIF Time type consisting of 6 Digits representing a UTC time in HHMMSS format or 4 Digits 
        /// representing a UTC time in HHMM format.
        /// </summary>
        public const string Time = "Time";

        /// <summary>
        /// ADIF String type consisting of a sequence of Characters.
        /// </summary>
        public const string String = "String";

        /// <summary>
        /// ADIF IntlString type consisting of a sequence of International Characters. 
        /// </summary>
        public const string IntlString = "IntlString";

        /// <summary>
        /// ADIF MultilineString type consisting of a sequence of Characters and line-breaks, where a line break is an ASCII CR (code 13) 
        /// followed immediately by an ASCII LF (code 10).
        /// </summary>
        public const string MultilineString = "MultilineString";

        /// <summary>
        /// ADIF IntlMultilineString type consisting of a sequence of International Characters and line breaks. 
        /// </summary>
        public const string IntlMultilineString = "IntlMultilineString";

        /// <summary>
        /// ADIF Enumeration type consisting of an explicit list of legal case-insensitive values represented in ASCII 
        /// in set notation, e.g. {A, B, C, D}, or defined in a table.
        /// </summary>
        public const string Enumeration = "Enumeration";

        /// <summary>
        /// ADIF Location type consisting of a sequence of 11 characters representing a latitude or longitude in XDDD MM.MMM format, 
        /// where X is a directional character from the set {E, W, N, S}, DDD is a 3-digit degrees specifier, where 
        /// 0 &lt;= DDD &lt;= 180, and MM.MMM is an unsigned Number minutes specifier with its decimal point 
        /// in the third position, where 00.000 &lt;= MM.MMM &lt;= 59.999. 
        /// </summary>
        public const string Location = "Location";

        /// <summary>
        /// ADIF GridSquare type consisting of a case-insensitive 2-character, 4-character, 6-character, or 8-character Maidenhead locator.
        /// </summary>
        public const string GridSquare = "GridSquare";

        /// <summary>
        /// ADIF GridSquareExt.
        /// </summary>
        public const string GridSquareExt = "GridSquareExt";

        /// <summary>
        /// ADIF GridSquareList type consisting of a comma-delimited list of <see cref="GridSquare"/> items.
        /// </summary>
        public const string GridSquareList = "GridSquareList";

        /// <summary>
        /// ADIF PositiveInteger type: an unsigned sequence of one or more <see cref="Digit"/>s representing a decimal 
        /// integer that has a value greater than 0.
        /// </summary>
        public const string PositiveInteger = "PositiveInteger";

        /// <summary>
        /// ADIF SecondarySubdivisionList type: a colon-delimited list of two or more members of the ADIF Secondary_Administrative_Subdivision enumeration.
        /// </summary>
        public const string SecondarySubdivisionList = "SecondarySubdivisionList";

        /// <summary>
        /// A semicolon (;) delimited, unordered list of one or more members of a Secondary_Administrative_Subdivision_Alt enumeration in the form:
        /// enumeration-name:enumeration-code
        /// </summary>
        public const string SecondaryAdministrativeSubdivisionListAlt = "SecondaryAdministrativeSubdivisionListAlt";

        /// <summary>
        /// ADIF SOTARef type: a sequence of <see cref="Character"/>s representing an International SOTA Reference.
        /// </summary>
        public const string SotaRef = "SOTARef";

        /// <summary>
        /// ADIF IOTARefNo type: an IOTA designator, in format CC-XXX, where CC is a member of the ADIF Continent enumeration
        /// and XXX is the island group designator, where 1 &lt;= XXX &lt;= 999 with leading zeros.
        /// </summary>
        public const string IotaRefNo = "IOTARefNo";

        /// <summary>
        /// A sequence of case-insensitive Characters representing a Parks on the Air reference in the form xxxx-nnnnn[@yyyyyy] 
        /// comprising 6 to 17 characters.
        /// <para>xxxx is the POTA national program and is 1 to 4 characters in length, typically the default callsign prefix of 
        /// the national program(rather than the DX entity)</para>
        /// <para>nnnnn represents the unique number within the national program and is either 4 or 5 characters in length</para>
        /// <para>yyyyyy is an optional value containing the 4 to 6 character ISO 3166-2 code to differentiate which state/province/prefecture/primary 
        /// administration location the contact represents, in the case that the park reference spans more than one location (such as a trail).</para>
        /// </summary>
        public const string PotaRef = "POTARef";

        /// <summary>
        /// A comma-delimited list of one or more POTARef items.
        /// </summary>
        public const string PotaRefList = "POTARefList";

        /// <summary>
        /// A sequence of case-insensitive Characters representing an International World Wildlife Flora &amp; Fauna reference in the 
        /// form xxFF-nnnn comprising 8 to 11 characters.
        /// <para>xx is the WWFF national program and is 1 to 4 characters in length</para>
        /// <para>FF- is two F characters followed by a dash character.</para>
        /// <para>nnnn represents the unique number within the national program and is 4 characters in length with leading zeros</para>
        /// </summary>
        public const string WwFfRef = "WWFFRef";

        /// <summary>
        /// ADIF Integer type: a sequence of one or more <see cref="Digit"/>s representing a decimal integer, optionally preceded 
        /// by a minus sign (ASCII code 45).
        /// </summary>
        public const string Integer = "Integer";
    }
}
