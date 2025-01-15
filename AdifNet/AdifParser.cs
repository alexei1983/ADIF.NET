using System.Text;
using org.goodspace.Data.Radio.Adif.Tags;
using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Parses a file, a stream, or plain text in the ADIF format.
    /// </summary>
    public class AdifParser
    {
        readonly IProgress<int>? progress;

        /// <summary>
        /// Creates a new instance of the <see cref="AdifParser"/> class.
        /// </summary>
        public AdifParser() { }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifParser"/> class.
        /// </summary>
        /// <param name="progress">Progress indicator.</param>
        public AdifParser(IProgress<int> progress)
        {
            this.progress = progress;
        }

        /// <summary>
        /// Prepares the specified file for parsing.
        /// </summary>
        /// <param name="path">Full path to the ADIF file that will be parsed.</param>
        public void LoadFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("File path is required.", nameof(path));

            if (!Path.IsPathRooted(path))
                path = Path.GetFullPath(path);

            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
                throw new FileNotFoundException($"ADIF file '{path}' does not exist.", path);

            data = File.ReadAllText(path);
        }

        /// <summary>
        /// Prepares the specified string for parsing.
        /// </summary>
        /// <param name="str">String containing the ADIF data that will be parsed.</param>
        public void Load(string str)
        {
            data = str ?? string.Empty;
        }

        /// <summary>
        /// Prepares the specified string for parsing.
        /// </summary>
        /// <param name="str">String containing the ADIF data that will be parsed.</param>
        public void LoadString(string str)
        {
            Load(str);
        }

        /// <summary>
        /// Prepares the specified stream for parsing.
        /// </summary>
        /// <param name="stream">Stream from which ADIF data will be read and parsed.</param>
        public void LoadStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream), "Stream cannot be null.");

            if (!stream.CanRead)
                throw new ArgumentException("Stream is not readable.", nameof(stream));

            this.data = string.Empty;
            var byteLength = stream.Length;
            var bytesRead = 0;
            var bytesToRead = byteLength < 10 ? (int)byteLength : 10;
            byte[] data = new byte[byteLength];

            using (stream)
            {
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);

                do
                {
                    var n = stream.Read(data, bytesRead, bytesToRead);
                    bytesRead += n;
                    byteLength -= n;
                } while (byteLength > 0);
            }

            if (data != null && data.Length > 0)
                this.data = Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="userDefinedTag"></param>
        bool IsUserDefinedField(string tagName, out UserDefTag? userDefinedTag)
        {
            userDefinedTag = userDefinedFields.FirstOrDefault(u => u.FieldName.Equals(tagName,
                                                                                      StringComparison.OrdinalIgnoreCase));
            return userDefinedTag != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="appDefinedTag"></param>
        bool IsAppDefinedField(string tagName, out AppDefTag? appDefinedTag)
        {
            appDefinedTag = appDefinedFields.FirstOrDefault(a => a.FieldName.Equals(tagName,
                                                                                    StringComparison.OrdinalIgnoreCase));
            return appDefinedTag != null;
        }

        /// <summary>
        /// Reads the file, text, or stream as ADIF and returns the resulting data set.
        /// </summary>
        public AdifDataSet Parse()
        {
            headers = [];
            body = [];
            userDefinedFields = [];
            appDefinedFields = [];
            headerInternal = [];

            if (string.IsNullOrWhiteSpace(data))
                throw new AdifParseException("No ADIF data found.");

            Initialize();
            var qsoCount = -1;

            while (i < data.Length)
            {
                var qso = GetQSO();

                if (qso != null && qso.Count > 0)
                {
                    body.Add(++qsoCount, qso);
                }
            }

            ReportProgress(true);

            var result = new AdifDataSet
            {
                Header = headerInternal,
                Qsos = []
            };

            foreach (var key in body.Keys)
            {
                var currentQso = body[key];
                var qso = new AdifQso();
                foreach (var entry in currentQso)
                {
                    // get the tag name and build it
                    var tag = TagFactory.TagFromName(entry.Key);

                    if (tag == null)
                    {
                        if (IsUserDefinedField(entry.Key, out UserDefTag? userTag) && userTag != null)
                            tag = new UserDefValueTag(userTag);
                        else if (IsAppDefinedField(entry.Key, out AppDefTag? appTag) && appTag != null)
                            tag = appTag.Clone() as AppDefTag;
                        else
                            throw new AdifParseException($"Unknown ADIF tag: {entry.Key}");
                    }

                    if (tag != null)
                    {
                        tag.SetValue(entry.Value);
                        qso.Add(tag);
                    }
                }

                result.Qsos.Add(qso);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        void Initialize()
        {
            // find the position of <EOH>
            var headerEndingPos = data.IndexOf($"{Values.TAG_OPENING}{AdifTags.EndHeader}{Values.TAG_CLOSING}",
                                                    StringComparison.OrdinalIgnoreCase);

            // if a header is not present, we can return from the method
            if (headerEndingPos < 0)
                return;

            i = 0;
            var tag = string.Empty;
            var valueLength = string.Empty;
            var value = string.Empty;
            var isUserFieldDef = false;
            var fieldId = -1;
            var userDefDataType = string.Empty;

            while (i < headerEndingPos)
            {
                // skip comments
                if (data[i] == Values.COMMENT_INDICATOR)
                {
                    while (i < headerEndingPos)
                    {
                        if (data[i] == Values.NEWLINE)
                        {
                            lineNumber++;
                            break;
                        }
                        i++;
                    }
                }
                else
                {
                    // find the beginning of a tag
                    if (data[i] == Values.TAG_OPENING)
                    {
                        i++;

                        // record the key
                        while (i < headerEndingPos && data[i] != Values.VALUE_LENGTH_CHAR)
                        {
                            if (data[i] == Values.NEWLINE)
                                lineNumber++;

                            tag = $"{tag}{data[i]}"; //tag + this.data[this.i];
                            i++;

                            // handle user-defined field definitions
                            if (tag.Equals(AdifTags.UserDef, StringComparison.OrdinalIgnoreCase))
                            {
                                isUserFieldDef = true;
                                var fieldNumber = string.Empty;

                                // read the field ID
                                while (i < headerEndingPos && data[i] != Values.VALUE_LENGTH_CHAR)
                                {
                                    if (data[i] == Values.NEWLINE)
                                        lineNumber++;

                                    fieldNumber += data[i];
                                    i++;
                                }

                                // iterate past the :
                                i++;

                                fieldId = fieldNumber.ToInt32();

                                // read the value length
                                while (i < headerEndingPos && data[i] != Values.VALUE_LENGTH_CHAR)
                                {
                                    if (data[i] == Values.NEWLINE)
                                        lineNumber++;

                                    valueLength += data[i];
                                    i++;
                                }

                                // iterate past the :
                                i++;

                                // read the data type
                                while (i < headerEndingPos && data[i] != Values.TAG_CLOSING)
                                {
                                    if (data[i] == Values.NEWLINE)
                                        lineNumber++;

                                    userDefDataType += data[i];
                                    i++;
                                }

                                // iterate past the tag closing
                                i++;
                                break;
                            } // end if we found a user-defined field definition
                        }

                        if (!isUserFieldDef)
                        {
                            // iterate past the :
                            i++;

                            // find out how long the value is
                            while (i < headerEndingPos && data[i] != Values.TAG_CLOSING)
                            {
                                if (data[i] == Values.NEWLINE)
                                    lineNumber++;

                                valueLength += data[i];
                                i++;
                            }

                            // iterate past the tag closing >
                            i++;
                        }

                        var len = valueLength.ToInt32();

                        // copy the value into the buffer
                        while (len > 0 && i < headerEndingPos)
                        {
                            if (data[i] == Values.NEWLINE)
                                lineNumber++;

                            value += data[i];
                            len--;
                            i++;
                        };

                        if (!isUserFieldDef)
                            headers[tag.Trim().ToUpper()] = value;
                        else
                        {
                            var userDefLen = value.Length;
                            var x = 0;
                            var fieldName = string.Empty;
                            var curlyBraceVal = string.Empty;
                            string[] enumValues = [];
                            var min = 0d;
                            var max = 0d;

                            // read the field name
                            while (userDefLen > 0 && value[x] != Values.COMMA)
                            {
                                if (data[i] == Values.NEWLINE)
                                    lineNumber++;

                                fieldName += value[x];
                                userDefLen--;
                                x++;
                            };

                            while (userDefLen > 0 && value[x] != Values.CURLY_BRACE_OPEN)
                            {
                                if (data[i] == Values.NEWLINE)
                                    lineNumber++;

                                x++;
                                userDefLen--;
                            }

                            if (userDefLen > 0 && value[x] == Values.CURLY_BRACE_OPEN)
                            {
                                x++; // iterate past the curly braces

                                // read the value between the curly braces
                                while (userDefLen > 0 && value[x] != Values.CURLY_BRACE_CLOSE)
                                {
                                    if (data[i] == Values.NEWLINE)
                                        lineNumber++;

                                    curlyBraceVal += value[x];
                                    userDefLen--;
                                    x++;
                                };
                            }

                            if (!string.IsNullOrWhiteSpace(curlyBraceVal))
                            {
                                // determine how to parse the optional curly brace string (e.g. as enum or range)
                                if (DataTypes.Enumeration.Equals(userDefDataType, StringComparison.OrdinalIgnoreCase) || curlyBraceVal.Contains(Values.COMMA))
                                {
                                    // split by comma
                                    enumValues = curlyBraceVal.Split(new[] { Values.COMMA }, StringSplitOptions.RemoveEmptyEntries);
                                }
                                else
                                {
                                    // parse as range
                                    if (curlyBraceVal.Contains(Values.COLON.ToString()))
                                    {
                                        var minMaxArray = curlyBraceVal.Split(new[] { Values.COLON }, StringSplitOptions.RemoveEmptyEntries);

                                        if (minMaxArray.Length == 2)
                                        {
                                            min = minMaxArray[0].ToDouble();
                                            max = minMaxArray[1].ToDouble();
                                        }
                                    }
                                    else
                                    {
                                        throw new AdifParseException($"Invalid range or enumeration definition in user-defined field {fieldName ?? string.Empty}.");
                                    }
                                }
                            }

                            userDefinedFields.Add(new UserDefTag()
                            {
                                FieldId = fieldId,
                                FieldName = fieldName,
                                UpperBound = max,
                                LowerBound = min,
                                CustomOptions = enumValues ?? [],
                                DataType = userDefDataType?.ToUpper() ?? string.Empty
                            });

                        } // end if we are processing a user-defined field definition

                        // clear our variables
                        tag = string.Empty;
                        valueLength = string.Empty;
                        value = string.Empty;
                        isUserFieldDef = false;
                        userDefDataType = string.Empty;
                        fieldId = -1;
                    }
                }

                i++;
                ReportProgress();
            }

            // iterate past the <EOH> header ending tag
            i = headerEndingPos + 5;

            if (data[i] == Values.NEWLINE)
                lineNumber++;

            // parse the header tags into ITag objects
            foreach (var header in headers)
            {
                // get the tag name and build it
                var headerTag = TagFactory.TagFromName(header.Key);

                if (headerTag != null && headerTag.Header)
                {
                    headerTag.SetValue(header.Value);
                    headerInternal.Add(headerTag);
                }
            }

            // add the user-defined fields to the headers
            foreach (var userDefined in userDefinedFields)
                headerInternal.Add(userDefined);

            ReportProgress();

            if (i >= data.Length)
                throw new AdifParseException("ADIF data contains no QSO records.");

        } // end method

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, string> GetQSO()
        {
            if (i >= data.Length)
                return [];
            var end = data.IndexOf($"{Values.TAG_OPENING}{AdifTags.EndRecord}{Values.TAG_CLOSING}",
                                    i,
                                    StringComparison.OrdinalIgnoreCase);

            if (end < 0)
            { // is this the end?
                i++;
                return [];
            }

            string? record = data[i..end];
            i = end + 5;

            ReportProgress();

            return GetQSOArray(record);
        } // end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Dictionary<string, string> GetQSOArray(string record)
        {
            var retVal = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(record))
                return retVal;

            for (var a = 0; a < record.Length; a++)
            {
                if (record[a] == Values.TAG_OPENING)
                {
                    var tagName = string.Empty;
                    var value = string.Empty;
                    var len_str = string.Empty;
                    var len = 0;

                    // app-defined variables
                    var programId = string.Empty;
                    var appFieldName = string.Empty;
                    var appFieldDataType = string.Empty;
                    var appFieldLength = string.Empty;
                    var isAppField = false;

                    a++; //go past the tag opening <

                    // get the tag
                    while (record[a] != Values.VALUE_LENGTH_CHAR)
                    {
                        tagName += record[a]; // append this char to the tag name
                        a++;

                        // look for app-defined tag
                        if (tagName.Equals(AdifTags.AppDef, StringComparison.OrdinalIgnoreCase))
                        {
                            isAppField = true;

                            // read the program ID
                            while (a < record.Length && record[a] != Values.UNDERSCORE)
                            {
                                programId += record[a];
                                a++;
                            }

                            // bypass the underscore
                            if (record[a] == Values.UNDERSCORE)
                                a++;

                            // read the field name (until we hit a colon)
                            while (a < record.Length && record[a] != Values.VALUE_LENGTH_CHAR)
                            {
                                appFieldName += record[a];
                                a++;
                            }

                            // read the length and data type
                            if (record[a] == Values.VALUE_LENGTH_CHAR)
                                a++;

                            while (a < record.Length && record[a] != Values.VALUE_LENGTH_CHAR && record[a] != Values.TAG_CLOSING)
                            {
                                appFieldLength += record[a];
                                a++;
                            }

                            len = appFieldLength.ToInt32();

                            var dataTypePresent = record[a] == Values.VALUE_LENGTH_CHAR;

                            // bypass unnecessary characters
                            if (record[a] == Values.VALUE_LENGTH_CHAR) //|| record[a] == Values.TagClosing)
                                a++;

                            // read the data type, if one is present
                            if (dataTypePresent)
                            {
                                while (a < record.Length && record[a] != Values.TAG_CLOSING)
                                {
                                    appFieldDataType += record[a];
                                    a++;
                                }
                            }

                            // add to the list of app-defined fields, if we haven't already
                            if (!IsAppDefinedField(appFieldName, out _))
                                appDefinedFields.Add(new AppDefTag()
                                {
                                    DataType = appFieldDataType,
                                    FieldName = appFieldName,
                                    ProgramId = programId
                                });

                            break;

                        }
                    }

                    if (!isAppField)
                    {
                        a++; // iterate past the : (value separator)

                        while (record[a] != Values.TAG_CLOSING && record[a] != Values.VALUE_LENGTH_CHAR)
                        {
                            len_str += record[a];
                            a++;
                        };
                        if (record[a] == Values.VALUE_LENGTH_CHAR)
                        {
                            while (record[a] != Values.TAG_CLOSING)
                            {
                                a++;
                            }
                        }
                        len = len_str.ToInt32();
                    }

                    // read the field value
                    while (len > 0)
                    {
                        a++;
                        value += record[a];
                        len--;
                    }

                    if (isAppField)
                        retVal[appFieldName.Trim()] = value;
                    else
                        retVal[tagName.Trim().ToUpper()] = value;
                }

                // skip comments
                if (record[a] == Values.COMMENT_INDICATOR)
                {
                    while (a < record.Length)
                    {
                        if (record[a] == Values.NEWLINE)
                        {
                            break;
                        }
                        a++;
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        void ReportProgress(bool done = false)
        {
            if (progress == null)
                return;

            var progressRaw = (double)i / data.Length * 100.0;

            if (progressRaw > 0 || done)
                progress.Report(done ? int.MaxValue : progressRaw > int.MaxValue ? int.MaxValue : (int)progressRaw);
        }

        string data = string.Empty;
        int i;
        int lineNumber;
        Dictionary<string, string> headers = [];
        Dictionary<int, Dictionary<string, string>> body = [];
        List<UserDefTag> userDefinedFields = [];
        List<AppDefTag> appDefinedFields = [];
        AdifHeader headerInternal = [];
    }
}
