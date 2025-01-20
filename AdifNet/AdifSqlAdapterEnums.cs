

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Determines how reserved words are escaped in a specific database implementation.
    /// </summary>
    public enum SqlReservedWordEscape
    {
        /// <summary>
        /// Reserved words are escaped by brackets.
        /// <code>[]</code>
        /// </summary>
        Brackets,

        /// <summary>
        /// Reserved words are escaped by double quotes.
        /// <code>"</code>
        /// </summary>
        DoubleQuotes,

        /// <summary>
        /// Reserved words are escaped by single quotes.
        /// <code>'</code>
        /// </summary>
        SingleQuotes,

        /// <summary>
        /// Reserved words are escaped by backticks.
        /// <code>`</code>
        /// </summary>
        Backticks
    }

    /// <summary>
    /// The character denoting the start of a SQL parameter in a specific database implementation.
    /// </summary>
    public enum SqlParameterPrefix
    {
        /// <summary>
        /// @ character parameter prefix.
        /// </summary>
        At,

        /// <summary>
        /// Question mark parameter prefix.
        /// </summary>
        Question,

        /// <summary>
        /// Colon parameter prefix.
        /// </summary>
        Colon,

        /// <summary>
        /// Semicolon parameter prefix.
        /// </summary>
        Semicolon,

        /// <summary>
        /// No parameter prefix.
        /// </summary>
        None
    }
}
