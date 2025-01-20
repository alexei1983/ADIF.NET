
namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// 
    /// </summary>
    public class AdifSqlAdapterSettings
    {
        /// <summary>
        /// Name of the database table containing QSOs.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Determines how reserved words in SQL are escaped.
        /// </summary>
        public SqlReservedWordEscape ReservedWordEscape { get; set; } = SqlReservedWordEscape.Brackets;

        /// <summary>
        /// Character that denotes the start of a database parameter.
        /// </summary>
        public SqlParameterPrefix ParameterPrefix { get; set; } = SqlParameterPrefix.At;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="reservedWordEscape"></param>
        /// <param name="parameterPrefix"></param>
        public AdifSqlAdapterSettings(string tableName, SqlReservedWordEscape reservedWordEscape, SqlParameterPrefix parameterPrefix)
        {
            ReservedWordEscape = reservedWordEscape;
            ParameterPrefix = parameterPrefix;
            TableName = Escape(tableName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetParameter(string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentException("Parameter name is required.", nameof(parameterName));

            return $"{GetParameterPrefix()}{parameterName}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Escape(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (IsTextEscaped(text))
                return text;

            return EscapeReservedWord(text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        char GetParameterPrefix()
        {
            return ParameterPrefix switch
            {
                SqlParameterPrefix.At => '@',
                SqlParameterPrefix.Question => '?',
                SqlParameterPrefix.Colon => ':',
                SqlParameterPrefix.Semicolon => ';',
                _ => ' ',
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        bool IsTextEscaped(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text value is required.", nameof(text));

            var escapeChars = GetReservedWordsEscapedBy();

            char startChar = escapeChars[0];
            char endChar = escapeChars.Length > 1 ? escapeChars[1] : escapeChars[0];

            if (!text.StartsWith(startChar))
                return false;

            if (!text.EndsWith(endChar))
                return false;

            string[] parts;

            if (text.Contains('.'))
                parts = text.Split('.');
            else
                parts = [text];

            foreach (var part in parts)
            {
                if (!part.StartsWith(startChar))
                    return false;

                if (!part.EndsWith(endChar))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        char[] GetReservedWordsEscapedBy()
        {
            return ReservedWordEscape switch
            {
                SqlReservedWordEscape.Brackets => ['[', ']'],
                SqlReservedWordEscape.DoubleQuotes => ['"'],
                SqlReservedWordEscape.SingleQuotes => ['\''],
                SqlReservedWordEscape.Backticks => ['`'],
                _ => throw new Exception(),
            };
        }

        /// <summary>
        /// Escapes the specified text to ensure it does not have special meaning 
        /// in SQL.
        /// </summary>
        /// <param name="text">Text value to escape.</param>
        string EscapeReservedWord(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text value is required.", nameof(text));

            if (IsTextEscaped(text))
                return text;

            string[] parts;

            if (text.Contains('.'))
                parts = text.Split('.');
            else
                parts = [text];

            var escapeChars = GetReservedWordsEscapedBy();

            char startChar = escapeChars[0];
            char endChar = escapeChars.Length > 1 ? escapeChars[1] : escapeChars[0];
            var val = string.Empty;

            foreach (var part in parts)
            {
                if (!string.IsNullOrEmpty(val) && parts.Length > 1)
                    val += '.';
                val += $"{startChar}{part}{endChar}";
            }

            return val;
        }
    }
}
